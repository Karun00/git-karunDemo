using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Threading;
using System.Threading.Tasks;
using Core.Repository.Providers.EntityFramework;
using System.Data.Entity;

namespace Core.Repository
{
    public class UnitOfWork : IUnitOfWorkAsync
    {
        #region Private Fields
        private DataContext _context;
        private readonly IDataContextAsync _dataContext;
        private bool _disposed;
        private ObjectContext _objectContext;
        private Dictionary<string, object> _repositories;
        private DbTransaction _transaction;
        private Database _database;
        #endregion Private Fields

        #region Constuctor/Dispose
        public UnitOfWork(IDataContextAsync dataContext) {
            //if (dataContext !=null)
            //{
            _dataContext = dataContext; 
            _database = dataContext.Database;
        //}
        }


        public DbContext DbContext
        {
            get
            {
                return _dataContext as DbContext;
            }
        }
        
            public void Dispose()
        {
            if (_objectContext != null && _objectContext.Connection.State == ConnectionState.Open)
            {
                _objectContext.Connection.Close();
            }

            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _dataContext.Dispose();
            }
            _disposed = true;
        }
        #endregion Constuctor/Dispose

        public int SaveChanges() { return _dataContext.SaveChanges(); }

        public IRepository<TEntity> Repository<TEntity>() where TEntity : IObjectState { return RepositoryAsync<TEntity>(); }

        public Task<int> SaveChangesAsync() { return _dataContext.SaveChangesAsync(); }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken) { return _dataContext.SaveChangesAsync(cancellationToken); }

        public IRepositoryAsync<TEntity> RepositoryAsync<TEntity>() where TEntity : IObjectState
        {
            if (_repositories == null)
            {
                _repositories = new Dictionary<string, object>();
            }

            var type = typeof(TEntity).Name;

            if (_repositories.ContainsKey(type))
            {
                return (IRepositoryAsync<TEntity>)_repositories[type];
            }

            var repositoryType = typeof(Repository<>);

            _repositories.Add(type, Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _dataContext, this));
            return (IRepositoryAsync<TEntity>)_repositories[type];
        }

        #region Unit of Work Transactions
        //IF 04/09/2014 Add IsolationLevel
        public void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Unspecified)
        {
            _objectContext = ((IObjectContextAdapter)_dataContext).ObjectContext;
            if (_objectContext.Connection.State != ConnectionState.Open)
            {
                _objectContext.CommandTimeout = 999;
                _objectContext.Connection.Open();
            }

            _transaction = _objectContext.Connection.BeginTransaction();
        }

        public bool Commit()
        {
            _transaction.Commit();
            return true;
        }

        public void Rollback()
        {
            _transaction.Rollback();
            ((DataContext)_dataContext).SyncObjectsStatePostCommit();
        }
        #endregion

        #region Raw Sql Query and Commands 
        
        //http://msdn.microsoft.com/en-in/data/jj592907.aspx

        public DbRawSqlQuery<TElement> SqlQuery<TElement>(string sql, params object[] parameters)
        {
            
            return _dataContext.Database.SqlQuery<TElement> (sql, parameters);
        }

        public DbRawSqlQuery SqlQuery(Type elementType, string sql, params object[] parameters)
        { 
           return  _dataContext.Database.SqlQuery(elementType, sql, parameters);
        }
        public int ExecuteSqlCommand(string sql, params object[] parameters)
        {
            return _dataContext.Database.ExecuteSqlCommand(sql, parameters);
        }

        #endregion

        #region Stored Procedure
        public IEnumerable<T> ExecuteStoredProcedure<T>(object storedProcedure)
        {
            return _dataContext.Database.ExecuteStoredProcedure<T>(storedProcedure);
        }
        #endregion

        // Uncomment, if rather have IRepositoryAsync<TEntity> IoC vs. Reflection Activation
        //public IRepositoryAsync<TEntity> RepositoryAsync<TEntity>() where TEntity : EntityBase
        //{
        //    return ServiceLocator.Current.GetInstance<IRepositoryAsync<TEntity>>();
        //}
    }
}