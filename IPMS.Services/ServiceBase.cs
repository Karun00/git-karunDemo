using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading;
using IPMS.Domain.Models;
using log4net;
using log4net.Config;
using Core.Repository;
using IPMS.Repository;
using IPMS.Domain;
using IPMS.Core.Repository.Exceptions;
using System.Diagnostics;
using System.Globalization;
using log4net.Appender;
using System.Configuration;
using System.Reflection;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq.Expressions;
using System.Data.Entity.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace IPMS.Services
{
    public class ServiceBase //: IDisposable
    {
        //private readonly ILog log;
        private static readonly ILog log = LogManager.GetLogger(typeof(ServiceBase));

        protected IUnitOfWork _unitOfWork;
        private IUserRepository _userRepository;

        public string _LoginName = string.Empty;
        public int _UserId = 0;
        public string _UserType = string.Empty;
        public readonly ICommonService _icommonservice;

        public string _PortCode = string.Empty;

        public ServiceBase()
        {
            log4net.Config.XmlConfigurator.Configure();
            log.Debug("Service Base");

            OperationContext context = OperationContext.Current;
            if (context != null)
            {
                try
                {
                    if (OperationContext.Current.IncomingMessageHeaders.FindHeader("LoginName", "System") == -1)
                    {
                        _LoginName = string.Empty;

                    }
                    else
                    {
                        _LoginName = OperationContext.Current.IncomingMessageHeaders.GetHeader<string>("LoginName", "System");
                    }

                    if (OperationContext.Current.IncomingMessageHeaders.FindHeader("PortCode", "System") == -1)
                    {
                        _PortCode = string.Empty;

                    }
                    else
                    {

                        _PortCode = OperationContext.Current.IncomingMessageHeaders.GetHeader<string>("PortCode", "System");
                    }
                }
                catch
                {
                    if (OperationContext.Current.Channel.State == CommunicationState.Faulted)
                    {
                        OperationContext.Current.Channel.Abort();

                    }
                }
            }

        }

        protected int GetUserIdByLoginname(string loginname)
        {

            if (_unitOfWork != null && !string.IsNullOrEmpty(loginname))
            {
                _userRepository = new UserRepository(_unitOfWork);
                _UserId = _userRepository.GetUserIdByLoginname(loginname);

            }

            return _UserId;
        }


        protected string GetUserType(string loginname)
        {
            if (_unitOfWork != null && !string.IsNullOrEmpty(loginname))
            {
                _userRepository = new UserRepository(_unitOfWork);
                _UserType = _userRepository.GetUserType(loginname);
            }
            return _UserType;
        }

        protected T ExecuteFaultHandledOperation<T>(Func<T> codetoExecute)
        {

            try
            {
                Stopwatch wfstopwatch;

                wfstopwatch = Stopwatch.StartNew();
                T response = default(T);
                if (codetoExecute != null)
                {

                    response = codetoExecute.Invoke();
                }
                wfstopwatch.Stop();

                string _class = codetoExecute.Target.ToString().Split('.')[2].ToString();
                string method = codetoExecute.Method.ToString();

                log.Info(string.Format("Time taken to execute class / method : {0} / {1} is {2} (Milli seconds) ", _class, method, wfstopwatch.ElapsedMilliseconds.ToString(CultureInfo.InvariantCulture)));


                return response;
            }
            catch (BusinessExceptions ex)
            {
                log.Error(LogErrorMessage("Business Validation : ", ex));
                throw new BusinessExceptions(ex.Message);
            }
            catch (Exception ex)
            {
                log.Error(LogErrorMessage(BusinessExceptions.InternalServerErrorMessage, ex));

                throw new FaultException(BusinessExceptions.InternalServerErrorMessage);
            }
            finally
            {
                if (_unitOfWork != null)
                {
                    _unitOfWork.Dispose();
                }
            }

        }

        protected void ExecuteFaultHandledOperation(Action codetoExecute)
        {
            try
            {
                Stopwatch wfstopwatch;
                wfstopwatch = Stopwatch.StartNew();
                if (codetoExecute != null)
                {
                    codetoExecute.Invoke();
                }
                wfstopwatch.Stop();

                string _class = codetoExecute.Target.ToString().Split('.')[2].ToString();
                string method = codetoExecute.Method.ToString();

                log.Info(string.Format("Time taken to execute class / method : {0} / {1} is {2} (Milli seconds) ", _class, method, wfstopwatch.ElapsedMilliseconds.ToString(CultureInfo.InvariantCulture)));

            }
            catch (BusinessExceptions ex)
            {
                log.Error(LogErrorMessage("Business Validation : ", ex));

                throw new BusinessExceptions(ex.Message);
            }
            catch (Exception ex)
            {
                log.Error(LogErrorMessage(BusinessExceptions.InternalServerErrorMessage, ex));
                throw new FaultException(BusinessExceptions.InternalServerErrorMessage);

            }
            finally
            {
                if (_unitOfWork != null)
                {
                    _unitOfWork.Dispose();
                }
            }

        }

        protected T EncloseTransactionAndHandleException<T>(Func<T> codeToExecute)
        {
            object obj1 = new object();
            try
            {

                Stopwatch wfstopwatch;
                wfstopwatch = Stopwatch.StartNew();

                _unitOfWork.BeginTransaction();
                T response = default(T);
                if (codeToExecute != null)
                {
                    //karuncode
                    // Capture relevant information from the target object
                    var target = codeToExecute.Target;
                    if (target == null)
                    {
                        throw new InvalidOperationException("Target object is null.");
                    }


                   
                     

                    // updated data and Model Name Purpose This Method
                    string ModelName = "";
                    object UpdatedData = GetRelevantFieldValueAndTypeName(target, out ModelName);
                     

                    //Getting Primary Key Code Purpose this Method
                    var result = GetPrimaryKeyName(ModelName, UpdatedData);

                    var context = _unitOfWork.DbContext;

                  var Res= GettingDataByPrimarykey(ModelName,result.PrimaryKeyName,result.PrimaryKeyValue);







                    //if (!string.IsNullOrEmpty(firstFieldTypeName))
                    //{
                    //    Type modelType = Type.GetType($"IPMS.Domain.Models.{firstFieldTypeName}, IPMS.Domain");
                    //    if (modelType != null)
                    //    {
                    //        var result = GetPrimaryKeyName(modelType, firstFieldTypeName, UpdatedData);
                    //        if (result.PrimaryKeyName != null)
                    //        {
                    //            var resultNotUpdated = GettingDataByPrimarykey(firstFieldTypeName, result.PrimaryKeyName, result.PrimaryKeyValue);
                    //            obj1 = resultNotUpdated;
                    //            if (obj1 != null)
                    //            {


                    //                var Ret = _userRepository.WithoutUpdatedJsonData1(obj1, firstFieldTypeName, UpdatedData);
                    //            }
                    //        }
                    //    }
                    //}




                    //
                    response = codeToExecute.Invoke();
                }
                _unitOfWork.Commit();

                wfstopwatch.Stop();
                string _class = codeToExecute.Target.ToString().Split('.')[2].ToString();
                string method = codeToExecute.Method.ToString();

                log.Info(string.Format("Time taken to execute class / method : {0} / {1} is {2} (Milli seconds) ", _class, method, wfstopwatch.ElapsedMilliseconds.ToString(CultureInfo.InvariantCulture)));


                return response;


            }
            catch (BusinessExceptions ex)
            {
                _unitOfWork.Rollback();
                log.Debug(ex.InnerException);
                log.Error(LogErrorMessage("Exception : ", ex));
                throw new BusinessExceptions(ex.Message);
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                log.Debug(ex.InnerException);
                log.Error(LogErrorMessage(BusinessExceptions.InternalServerErrorMessage, ex));
                throw new FaultException(BusinessExceptions.InternalServerErrorMessage);
            }
            finally
            {

                if (_unitOfWork != null)
                {
                    _unitOfWork.Dispose();
                }
            }

        }

        //protected T EncloseTransactionAndHandleException<T>(Func<T> codeToExecute)
        //{
        //    object obj1 = new object();
        //    try
        //    {

        //        Stopwatch wfstopwatch;
        //        wfstopwatch = Stopwatch.StartNew();

        //        _unitOfWork.BeginTransaction();
        //        T response = default(T);
        //        if (codeToExecute != null)
        //        {
        //            //karuncode
        //            // Capture relevant information from the target object
        //            var target = codeToExecute.Target;
        //            if (target == null)
        //            {
        //                throw new InvalidOperationException("Target object is null.");
        //            }

        //            object dataValue = Getting_UpdatedData(target);
        //            string firstFieldTypeName = GetFirstFieldTypeName(target);
        //            if (!string.IsNullOrEmpty(firstFieldTypeName))
        //            {
        //                Type modelType = Type.GetType($"IPMS.Domain.Models.{firstFieldTypeName}, IPMS.Domain");
        //                if (modelType != null)
        //                {
        //                    var result = GetPrimaryKeyName(modelType, firstFieldTypeName, dataValue);
        //                    if (result.PrimaryKeyName != null)
        //                    {
        //                        var resultNotUpdated = GettingDataByPrimarykey(firstFieldTypeName, result.PrimaryKeyName, result.PrimaryKeyValue);
        //                        obj1 = resultNotUpdated;
        //                        if(obj1 !=null)
        //                        {


        //                            var Ret = _userRepository.WithoutUpdatedJsonData1(obj1, firstFieldTypeName, dataValue);
        //                        }
        //                    }
        //                }
        //            }
        //            //
        //            response = codeToExecute.Invoke();
        //        }
        //        _unitOfWork.Commit();

        //        wfstopwatch.Stop();
        //        string _class = codeToExecute.Target.ToString().Split('.')[2].ToString();
        //        string method = codeToExecute.Method.ToString();

        //        log.Info(string.Format("Time taken to execute class / method : {0} / {1} is {2} (Milli seconds) ", _class, method, wfstopwatch.ElapsedMilliseconds.ToString(CultureInfo.InvariantCulture)));


        //        return response;


        //    }
        //    catch (BusinessExceptions ex)
        //    {
        //        _unitOfWork.Rollback();
        //        log.Debug(ex.InnerException);
        //        log.Error(LogErrorMessage("Exception : ", ex));
        //        throw new BusinessExceptions(ex.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        _unitOfWork.Rollback();
        //        log.Debug(ex.InnerException);
        //        log.Error(LogErrorMessage(BusinessExceptions.InternalServerErrorMessage, ex));
        //        throw new FaultException(BusinessExceptions.InternalServerErrorMessage);
        //    }
        //    finally
        //    {

        //        if (_unitOfWork != null)
        //        {
        //            _unitOfWork.Dispose();
        //        }
        //    }

        //}

        protected void EncloseTransactionAndHandleException(Action codeToExecute)
        {
            try
            {
                Stopwatch wfstopwatch;
                wfstopwatch = Stopwatch.StartNew();


                _unitOfWork.BeginTransaction();
                if (codeToExecute != null)
                {
                    codeToExecute.Invoke();
                }
                _unitOfWork.Commit();


                wfstopwatch.Stop();
                string _class = codeToExecute.Target.ToString().Split('.')[2].ToString();
                string method = codeToExecute.Method.ToString();

                log.Info(string.Format("Time taken to execute class / method : {0} / {1} is {2} (Milli seconds) ", _class, method, wfstopwatch.ElapsedMilliseconds.ToString(CultureInfo.InvariantCulture)));


            }
            catch (BusinessExceptions ex)
            {
                _unitOfWork.Rollback();
                log.Error(LogErrorMessage("Business Validation : ", ex));
                throw new BusinessExceptions(ex.Message);
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                log.Error(LogErrorMessage(BusinessExceptions.InternalServerErrorMessage, ex));
                throw new FaultException(BusinessExceptions.InternalServerErrorMessage);
            }
            finally
            {
                if (_unitOfWork != null)
                {
                    _unitOfWork.Dispose();
                }
            }

        }

        private string LogErrorMessage(string pretext, Exception ex)
        {
            string msg = pretext + " " + ex.Message;
            if (ex.InnerException != null)
            {
                if (!string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    msg = msg + " Inner Exception:" + ex.InnerException.Message;
                }
            }
            return msg;
        }
        //protected virtual void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        if (_unitOfWork != null)
        //        {
        //            _unitOfWork.Dispose();
        //        }
        //    }
        //}
        //public void Dispose()
        //{
        //    Dispose(true);
        //    GC.SuppressFinalize(this);
        //}


        //karun Code

        private object GetRelevantFieldValueAndTypeName(object target, out string typeName)
        {
            var targetType = target.GetType();
            var relevantField = targetType.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                                          .FirstOrDefault(f => IsRelevantField(f));

            if (relevantField != null)
            {
                typeName = relevantField.FieldType.Name;
                if (typeName.EndsWith("VO"))
                {
                    typeName = typeName.Substring(0, typeName.Length - 2);
                }
                return relevantField.GetValue(target);
            }

            typeName = string.Empty;
            return null;
        }

        private bool IsRelevantField(FieldInfo field)
        {
            if (field == null)
                return false;

            var fieldName = field.Name;
            return fieldName.IndexOf("data", StringComparison.OrdinalIgnoreCase) >= 0 ||
                   fieldName.IndexOf("Important", StringComparison.OrdinalIgnoreCase) >= 0;
        }


        
      
        
        public PrimaryKeyResult GetPrimaryKeyName(string modelName, object modelData)
        {
            var dbContext = _unitOfWork.DbContext;
            if (dbContext == null)
                throw new ArgumentNullException(nameof(dbContext));

            if (string.IsNullOrEmpty(modelName))
                throw new ArgumentException("Model name cannot be null or empty.", nameof(modelName));

            if (modelData == null)
                throw new ArgumentNullException(nameof(modelData));
           
            var objectContext = dbContext as IObjectContextAdapter;
            if (objectContext == null || objectContext.ObjectContext == null)
                throw new InvalidOperationException("Unable to get ObjectContext from DbContext.");
            
            var metadata = objectContext.ObjectContext.MetadataWorkspace;
            var entityContainer = metadata.GetItems<EntityContainer>(DataSpace.CSpace).FirstOrDefault();
            if (entityContainer == null)
                throw new InvalidOperationException("EntityContainer not found.");
         
            var entitySet = entityContainer.BaseEntitySets.OfType<EntitySet>()
                .FirstOrDefault(e => e.ElementType.Name == modelName);
            if (entitySet == null)
                throw new InvalidOperationException($"Entity set '{modelName}' not found.");
            
            var keyProperties = entitySet.ElementType.KeyMembers.Select(k => k.Name).ToList();
            if (!keyProperties.Any())
                throw new InvalidOperationException("No primary key found.");
            
            var primaryKeyName = keyProperties[0];
            var propertyInfo = modelData.GetType().GetProperty(primaryKeyName);
            if (propertyInfo == null)
                throw new InvalidOperationException($"Property '{primaryKeyName}' not found."); 
                     
            var primaryKeyValue = propertyInfo.GetValue(modelData)?.ToString();
            return new PrimaryKeyResult
            {
                PrimaryKeyName = primaryKeyName,
                PrimaryKeyValue = primaryKeyValue
            };

             

        }



        //public object GetModelData(string modelName)
        //{
        //    var dbContext = _unitOfWork.DbContext;

        //    // Find the DbSet property corresponding to the model name
        //    var dbSetProperty = dbContext.GetType().GetProperties()
        //        .FirstOrDefault(p => p.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>) &&
        //                             p.PropertyType.GetGenericArguments().First().Name == modelName);

        //    if (dbSetProperty == null)
        //    {
        //        throw new ArgumentException($"No DbSet found for model name: {modelName}");
        //    }

        //    // Get the DbSet instance
        //    var dbSet = dbSetProperty.GetValue(dbContext);

        //    var toListMethod = typeof(Enumerable).GetMethods()
        //.First(m => m.Name == "ToList" && m.GetParameters().Length == 1)
        //.MakeGenericMethod(dbSetProperty.PropertyType.GetGenericArguments().First());

        //    var data = toListMethod.Invoke(null, new[] { dbSet });

        //    return data;
        //}

        public IQueryable<object> GettingDataByPrimarykey(string modelName, string primaryKeyName, string primaryKeyValue)
        {
            // working 
            try
            {
                var assembly = AppDomain.CurrentDomain.GetAssemblies()
                    .FirstOrDefault(a => a.FullName.StartsWith("IPMS.Domain"));
                if (assembly == null)
                {
                    throw new Exception("Assembly not found.");
                }

                var type = assembly.GetType($"IPMS.Domain.Models.{modelName}");
                if (type == null)
                {
                    throw new Exception($"Type {modelName} not found in assembly.");
                }

                var repositoryMethod = typeof(IUnitOfWork).GetMethod(nameof(IUnitOfWork.Repository))
                    .MakeGenericMethod(type);
                var repository = repositoryMethod.Invoke(_unitOfWork, null);
                if (repository == null)
                {
                    throw new Exception($"Repository for {modelName} could not be retrieved.");
                }

                var repositoryType = repository.GetType();
                var queryableMethod = repositoryType.GetMethod("Queryable", Type.EmptyTypes);
                if (queryableMethod == null)
                {
                    throw new Exception("Queryable method not found on repository.");
                }

                var queryable = queryableMethod.Invoke(repository, new object[] { }) as IQueryable;
                if (queryable == null)
                {
                    throw new Exception("Queryable method returned null.");
                }

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
                var whereMethod = typeof(Queryable).GetMethods()
                    .First(m => m.Name == "Where" && m.GetParameters().Length == 2)
                    .MakeGenericMethod(type);

                var query = (IQueryable<object>)whereMethod.Invoke(null, new object[] { queryable, lambda });

                // var query = (IQueryable<object>)whereMethod.Invoke(null, new object[] {  lambda });



                return query;
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                return Enumerable.Empty<object>().AsQueryable();
            }
        }





        //private object Getting_UpdatedData1(object target)
        //{
        //    var targetType = target.GetType();
        //    var dataField = targetType.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
        //                              .FirstOrDefault(f => GetRelevanceInfo1(f) != null);

        //    return dataField?.GetValue(target);
        //}
        //private string GetRelevanceInfo1(MemberInfo member)
        //{
        //    FieldInfo field = member as FieldInfo;
        //    if (field != null)
        //    {

        //        if (field.Name.IndexOf("data", StringComparison.OrdinalIgnoreCase) >= 0 ||
        //            field.Name.IndexOf("Important", StringComparison.OrdinalIgnoreCase) >= 0)
        //        {
        //            return $"Field '{field.Name}' is relevant.";
        //        }
        //    }


        //    return field.Name;
        //}


        //private string GetFirstFieldTypeName1(object target)
        //{
        //    var targetType = target.GetType();
        //    var firstField = targetType.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
        //                               .FirstOrDefault(f => GetRelevanceInfo(f) != null);

        //    if (firstField != null)
        //    {
        //        string firstFieldTypeName = firstField.FieldType.Name;
        //        if (firstFieldTypeName.EndsWith("VO"))
        //        {
        //            firstFieldTypeName = firstFieldTypeName.Substring(0, firstFieldTypeName.Length - 2);
        //        }
        //        return firstFieldTypeName;
        //    }
        //    return string.Empty;
        //}

        //public PrimaryKeyResult GetPrimaryKeyName1(object obj, string modelName, object modelDataobj2)
        //{
        //    var dbContext = _unitOfWork.DbContext;

        //    if (dbContext == null)
        //    {
        //        throw new InvalidOperationException("DbContext is not available.");
        //    }

        //    var objectContext = ((IObjectContextAdapter)dbContext).ObjectContext;

        //    if (objectContext == null)
        //    {
        //        throw new InvalidOperationException("Unable to get ObjectContext from DbContext.");
        //    }

        //    // Get the metadata workspace
        //    var metadata = objectContext.MetadataWorkspace;

        //    // Find the EntityContainer
        //    var entityContainer = metadata.GetItems<EntityContainer>(DataSpace.CSpace)
        //        .FirstOrDefault();

        //    if (entityContainer == null)
        //    {
        //        throw new InvalidOperationException("EntityContainer not found in metadata workspace.");
        //    }

        //    // Find the entity set based on the model name
        //    var entitySet = entityContainer.BaseEntitySets
        //        .OfType<EntitySet>()
        //        .FirstOrDefault(e => e.ElementType.Name == modelName);

        //    if (entitySet != null)
        //    {
        //        // Get the primary key properties
        //        var keyProperties = entitySet.ElementType.KeyMembers.Select(k => k.Name).ToList();

        //        if (keyProperties.Any())
        //        {
        //            // Get the primary key name (assuming there's at least one key property)
        //            var primaryKeyName = keyProperties[0];
        //            var propertyInfo = modelDataobj2.GetType().GetProperty(primaryKeyName);

        //            if (propertyInfo != null)
        //            {
        //                // Get the primary key value
        //                var primaryKeyValue = propertyInfo.GetValue(modelDataobj2)?.ToString();

        //                // Return the primary key name and value
        //                return new PrimaryKeyResult
        //                {
        //                    PrimaryKeyName = primaryKeyName,
        //                    PrimaryKeyValue = primaryKeyValue
        //                };
        //            }
        //            else
        //            {
        //                throw new InvalidOperationException($"Property '{primaryKeyName}' not found in the provided object.");
        //            }
        //        }
        //        else
        //        {
        //            throw new InvalidOperationException("No primary key found for the given model.");
        //        }
        //    }
        //    else
        //    {
        //        throw new InvalidOperationException($"Entity set for model '{modelName}' not found.");
        //    }
        //}

        //public IQueryable<object> GettingDataByPrimarykey(string modelName, string primaryKeyName, string primaryKeyValue)
        //{
        //    // working 
        //    try
        //    {
        //        var assembly = AppDomain.CurrentDomain.GetAssemblies()
        //            .FirstOrDefault(a => a.FullName.StartsWith("IPMS.Domain"));
        //        if (assembly == null)
        //        {
        //            throw new Exception("Assembly not found.");
        //        }

        //        var type = assembly.GetType($"IPMS.Domain.Models.{modelName}");
        //        if (type == null)
        //        {
        //            throw new Exception($"Type {modelName} not found in assembly.");
        //        }

        //        var repositoryMethod = typeof(IUnitOfWork).GetMethod(nameof(IUnitOfWork.Repository))
        //            .MakeGenericMethod(type);
        //        var repository = repositoryMethod.Invoke(_unitOfWork, null);
        //        if (repository == null)
        //        {
        //            throw new Exception($"Repository for {modelName} could not be retrieved.");
        //        }

        //        var repositoryType = repository.GetType();
        //        var queryableMethod = repositoryType.GetMethod("Queryable", Type.EmptyTypes);
        //        if (queryableMethod == null)
        //        {
        //            throw new Exception("Queryable method not found on repository.");
        //        }

        //        var queryable = queryableMethod.Invoke(repository, new object[] { }) as IQueryable;
        //        if (queryable == null)
        //        {
        //            throw new Exception("Queryable method returned null.");
        //        }

        //        var parameter = Expression.Parameter(type, "x");
        //        var keyProperty = type.GetProperty(primaryKeyName);
        //        if (keyProperty == null)
        //        {
        //            throw new Exception($"Primary key property {primaryKeyName} not found on type {modelName}.");
        //        }

        //        var keyValue = Expression.Constant(Convert.ChangeType(primaryKeyValue, keyProperty.PropertyType), keyProperty.PropertyType);
        //        var property = Expression.Property(parameter, keyProperty);
        //        var equality = Expression.Equal(property, keyValue);
        //        var lambda = Expression.Lambda(equality, parameter);
        //        var whereMethod = typeof(Queryable).GetMethods()
        //            .First(m => m.Name == "Where" && m.GetParameters().Length == 2)
        //            .MakeGenericMethod(type);

        //        var query = (IQueryable<object>)whereMethod.Invoke(null, new object[] { queryable, lambda });

        //        // var query = (IQueryable<object>)whereMethod.Invoke(null, new object[] {  lambda });



        //        return query;
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception if needed
        //        return Enumerable.Empty<object>().AsQueryable();
        //    }
        //}


        //karun code End



    }

    [Serializable()]
    public class AppSettingsConnectionStringAdoNetAppender : AdoNetAppender
    {
        public override void ActivateOptions()
        {
            PopulateConnectionString();
            base.ActivateOptions();
        }

        private void PopulateConnectionString()
        {
            ConnectionString = ConnectionString;
        }

        public new string ConnectionString
        {
            get { return base.ConnectionString; }
            set
            {
                base.ConnectionString = ConfigurationManager.ConnectionStrings["TnpaContext"].ToString();
            }
        }
    }


    //karun code

    public class PrimaryKeyResult
    {
        public string PrimaryKeyName { get; set; }
        public string PrimaryKeyValue { get; set; }
    }
}
