using System;
using System.Data.Entity;

namespace Core.Repository
{
    public interface IDataContext : IDisposable
    {
        int SaveChanges();
        void SyncObjectState(object entity);
        Database Database { get; }
    }
}