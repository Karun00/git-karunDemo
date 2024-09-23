using IPMS.Core.Repository.Exceptions;
using System;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Core.Repository.Providers.EntityFramework
{
    public class DataContext : DbContext, IDataContextAsync
    {
        #region Private Fields
        private readonly IUnitOfWork _unitofwork;
        private readonly Guid _instanceId;
        #endregion Private Fields

        public DataContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
            _instanceId = Guid.NewGuid();
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        public Guid InstanceId { get { return _instanceId; } }

        public override int SaveChanges()
        {
            try
            {
                SyncObjectsStatePreCommit();
                var changes = base.SaveChanges();
                SyncObjectsStatePostCommit();
                return changes;
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                StringBuilder sbException = new StringBuilder();
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        System.Diagnostics.Trace.TraceInformation("Class: {0}, Property: {1}, Error: {2}",
                            validationErrors.Entry.Entity.GetType().FullName,
                            validationError.PropertyName,
                            validationError.ErrorMessage);
                        sbException.Append(validationError.ErrorMessage + ",");
                    }
                }
                string exception = sbException.ToString().Substring(0, sbException.ToString().Length - 1);
                raise = new CustomEntityValidationException(exception, raise);
                throw raise;  // You can also choose to handle the exception here...
            }
        }

        public override async Task<int> SaveChangesAsync()
        {
            SyncObjectsStatePreCommit();
            var changesAsync = await base.SaveChangesAsync();
            SyncObjectsStatePostCommit();
            return changesAsync;
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            SyncObjectsStatePreCommit();
            var changesAsync = await base.SaveChangesAsync(cancellationToken);
            SyncObjectsStatePostCommit();
            return changesAsync;
        }

        public void SyncObjectState(object entity) { Entry(entity).State = StateHelper.ConvertState(((IObjectState)entity).ObjectState); }
        public new DbSet<T> Set<T>() where T : class { return base.Set<T>(); }

        private void SyncObjectsStatePreCommit()
        {
            int i = 0;
            foreach (var dbEntityEntry in ChangeTracker.Entries())
            {
                dbEntityEntry.State = StateHelper.ConvertState(((IObjectState)dbEntityEntry.Entity).ObjectState);

                if (i == 0)
                {
                    var obj = dbEntityEntry.Entity; // Get the entity
                    var modelName = obj.GetType().Name; // Get the entity type name
                    var httpMethodName = dbEntityEntry.State.ToString(); // Get the state as string


                    CodeDifferenceFind(obj, modelName);

                    i++;
                }
            }
        }

        public void SyncObjectsStatePostCommit()
        {
            foreach (var dbEntityEntry in ChangeTracker.Entries())
            {
                ((IObjectState)dbEntityEntry.Entity).ObjectState = StateHelper.ConvertState(dbEntityEntry.State);
            }
        }




        //karun Code

        public void CodeDifferenceFind(object obj, string modelName)
        {
            if (obj == null) return;

            var context = ((IObjectContextAdapter)this).ObjectContext;
            var metadata = context.MetadataWorkspace;
            var entitySet = metadata.GetItems<EntityContainer>(DataSpace.CSpace)
                .SelectMany(c => c.BaseEntitySets)
                .FirstOrDefault(e => e.ElementType.Name == modelName);

            if (entitySet != null)
            {
                var keyProperties = entitySet.ElementType.KeyMembers.Select(k => k.Name).ToList();
                if (keyProperties.Any())
                {
                    var primaryKeyName = keyProperties[0];
                    var propertyInfo = obj.GetType().GetProperty(primaryKeyName);

                    if (propertyInfo != null)
                    {
                        string primaryKeyValue = propertyInfo.GetValue(obj).ToString();
                        if (primaryKeyValue != null)
                        {
                            GettingDataByPrimarykey1(modelName, primaryKeyName, primaryKeyValue);
                        }

                    }

                }
            }

        }
        public void GettingDataByPrimarykey(string modelName, string primaryKeyName, string primaryKeyValue)
        {
            try
            {
                // Load the assembly where the model is defined
                var assembly = AppDomain.CurrentDomain.GetAssemblies()
                    .FirstOrDefault(a => a.FullName.StartsWith("IPMS.Domain"));

                if (assembly == null)
                {
                    throw new Exception("Assembly not found.");
                }

                // Get the type from the assembly
                var type = assembly.GetType($"IPMS.Domain.Models.{modelName}");

                if (type == null)
                {
                    throw new Exception($"Type {modelName} not found in assembly.");
                }

                // Get the MethodInfo for Set<T>()
                var setMethod = typeof(DbContext)
                    .GetMethods(BindingFlags.Public | BindingFlags.Instance)
                    .FirstOrDefault(m => m.Name == "Set" && m.GetParameters().Length == 0);

                if (setMethod == null)
                {
                    throw new Exception("Set<T>() method not found.");
                }

                // Make generic and invoke Set<T>()
                var genericSetMethod = setMethod.MakeGenericMethod(type);
                var dbSet = genericSetMethod.Invoke(this, null);

                if (dbSet == null)
                {
                    throw new Exception("DbSet not created.");
                }

                // Ensure dbSet is IQueryable
                var queryableDbSet = dbSet as IQueryable;
                if (queryableDbSet == null)
                {
                    throw new Exception("DbSet is not IQueryable.");
                }

                // Find the type of 'SingleOrDefault'
                var queryableType = typeof(Queryable);
                var singleOrDefaultMethod = queryableType.GetMethods(BindingFlags.Static | BindingFlags.Public)
                    .FirstOrDefault(m => m.Name == "SingleOrDefault" && m.GetParameters().Length == 2);

                if (singleOrDefaultMethod == null)
                {
                    throw new Exception("SingleOrDefault method not found.");
                }

                // Define the parameter for the lambda expression
                var parameter = Expression.Parameter(type, "e");

                // Define the property expression
                var property = Expression.Property(parameter, primaryKeyName);

                // Convert the primary key value
                var keyValue = Convert.ChangeType(primaryKeyValue, type.GetProperty(primaryKeyName).PropertyType);

                // Define the constant expression
                var constant = Expression.Constant(keyValue);

                // Create the equality expression
                var equality = Expression.Equal(property, constant);

                // Define the lambda expression
                var lambda = Expression.Lambda(
                    typeof(Func<,>).MakeGenericType(type, typeof(bool)), // Use proper Func<> type
                    equality,
                    parameter
                );

                // Make generic and invoke SingleOrDefault<T>()
                var genericSingleOrDefaultMethod = singleOrDefaultMethod.MakeGenericMethod(type);
                var filteredEntity = genericSingleOrDefaultMethod.Invoke(null, new object[] { queryableDbSet, lambda });

                // Use the retrieved entity as needed
                Console.WriteLine($"Retrieved entity: {filteredEntity}");
            }
            catch (Exception ex)
            {
                // Handle exceptions as needed
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }


        public IQueryable GettingDataByPrimarykey1(string modelName, string primaryKeyName, object primaryKeyValue)
        {
            try
            {
                // Find the assembly that contains the model
                var assembly = AppDomain.CurrentDomain.GetAssemblies()
                    .FirstOrDefault(a => a.FullName.StartsWith("IPMS.Domain"));

                if (assembly == null)
                {
                    throw new Exception("Assembly not found.");
                }

                // Get the type of the model
                var type = assembly.GetType($"IPMS.Domain.Models.{modelName}");

                if (type == null)
                {
                    throw new Exception($"Type {modelName} not found in assembly.");
                }

                // Get the repository method from IUnitOfWork and invoke it
                var repositoryMethod = typeof(IUnitOfWork).GetMethod(nameof(IUnitOfWork.Repository))
                    .MakeGenericMethod(type);

                // Ensure _unitofwork is an instance of IUnitOfWork and not null
                if (_unitofwork == null)
                {
                    throw new Exception("Unit of work instance is null.");
                }

                var repository = repositoryMethod.Invoke(_unitofwork, null);

                if (repository == null)
                {
                    throw new Exception($"Repository for {modelName} could not be retrieved.");
                }

                // Get the Queryable property from the repository
                var queryableProperty = repository.GetType().GetProperty("Queryable");

                if (queryableProperty == null)
                {
                    throw new Exception("Queryable property not found on repository.");
                }

                var queryable = (IQueryable)queryableProperty.GetValue(repository);

                // Build the expression to filter by primary key
                var parameter = Expression.Parameter(type, "x");
                var keyProperty = type.GetProperty(primaryKeyName);
                if (keyProperty == null)
                {
                    throw new Exception($"Primary key property {primaryKeyName} not found on type {modelName}.");
                }

                var keyValue = Expression.Constant(Convert.ChangeType(primaryKeyValue, keyProperty.PropertyType), keyProperty.PropertyType);
                var property = Expression.Property(parameter, keyProperty);
                var equality = Expression.Equal(property, keyValue);
                var lambda = Expression.Lambda(equality, parameter);

                // Get the Where method and invoke it
                var whereMethod = typeof(Queryable).GetMethods()
                    .First(m => m.Name == "Where" && m.GetParameters().Length == 2)
                    .MakeGenericMethod(type);

                var query = (IQueryable)whereMethod.Invoke(null, new object[] { queryable, lambda });

                return query;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                Console.WriteLine($"Error: {ex.Message}");
                return Enumerable.Empty<object>().AsQueryable();
            }
        }




    }
}