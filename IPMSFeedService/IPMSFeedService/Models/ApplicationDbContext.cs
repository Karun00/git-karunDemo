using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;

namespace IPMSFeedService.Models
{
    public class ApplicationDbContexts : DbContext, IApplicationDbContext
    {
        //AuthenticationEntities db = new AuthenticationEntities();
        //public AuthenticationEntities ApplicationDbContext { get { return db; } }


         private readonly string _defaultSchema;
        private string UserName { get; set; }
        private bool _disposed;
        private ObjectContext _objectContext;
        private Dictionary<string, object> _repositories;
        private DbTransaction _transaction;
        //private IEventDispatcher _eventDispatcher;

        static ApplicationDbContexts()
        {
            Database.SetInitializer<ApplicationDbContexts>(null);
        }

        public ApplicationDbContexts()
            : base("Name=IPMSFeedContext")
        {
            Database.SetInitializer<ApplicationDbContexts>(null);
            Configuration.ProxyCreationEnabled = true;
            Configuration.LazyLoadingEnabled = true;
            Configuration.AutoDetectChangesEnabled = true;
            _defaultSchema = ConfigurationManager.AppSettings["defaultSchema"];

            //_eventDispatcher = eventDispatcher;
        }

        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        public void Dispose()
        {
            if (_objectContext != null)
            {
                if (_objectContext.Connection != null)
                {
                    if (_objectContext.Connection.State == ConnectionState.Open)
                    {
                        _objectContext.Connection.Close();
                    }
                }
            }

            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _objectContext = null;
                //  this.Dispose();
                //_dataContext.Dispose();
            }
            _disposed = true;
        }

    }
}