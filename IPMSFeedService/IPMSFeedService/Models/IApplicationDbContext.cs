using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace IPMSFeedService.Models
{
    public interface IApplicationDbContext
    {
        //int SaveChanges();

        //bool Commit();

        //void Rollback();

        //int ExecuteSqlCommand(string sql, params object[] parameters);

        //DbRawSqlQuery SqlQuery(Type elementType, string sql, params object[] parameters);
        //DbRawSqlQuery<TElement> SqlQuery<TElement>(string sql, params object[] parameters);

        //DbRawSqlQuery<TElement> SqlQuery<TElement>(string sql, params object[] parameters);
     

        void Dispose(bool disposing);

        void Dispose();


    }
}