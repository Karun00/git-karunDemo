using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Core.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        int SaveChanges();
        void Dispose(bool disposing);
        IRepository<TEntity> Repository<TEntity>() where TEntity : IObjectState;
        //IF 04/09/2014 Add IsolationLevel
        void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Unspecified);
        bool Commit();
        void Rollback();

        int ExecuteSqlCommand(string sql, params object[] parameters);
        //
        // Summary:
        //     Creates a raw SQL query that will return elements of the given generic type.
        //      The type can be any type that has properties that match the names of the
        //     columns returned from the query, or can be a simple primitive type. The type
        //     does not have to be an entity type. The results of this query are never tracked
        //     by the context even if the type of object returned is an entity type. Use
        //     the System.Data.Entity.DbSet<TEntity>.SqlQuery(System.String,System.Object[])
        //     method to return entities that are tracked by the context.  As with any API
        //     that accepts SQL it is important to parameterize any user input to protect
        //     against a SQL injection attack. You can include parameter place holders in
        //     the SQL query string and then supply parameter values as additional arguments.
        //     Any parameter values you supply will automatically be converted to a DbParameter.
        //      context.Database.SqlQuery<Post>("SELECT * FROM dbo.Posts WHERE Author =
        //     @p0", userSuppliedAuthor); Alternatively, you can also construct a DbParameter
        //     and supply it to SqlQuery. This allows you to use named parameters in the
        //     SQL query string.  context.Database.SqlQuery<Post>("SELECT * FROM dbo.Posts
        //     WHERE Author = @author", new SqlParameter("@author", userSuppliedAuthor));
        //
        // Parameters:
        //   sql:
        //     The SQL query string.
        //
        //   parameters:
        //     The parameters to apply to the SQL query string.
        //
        // Type parameters:
        //   TElement:
        //     The type of object returned by the query.
        //
        // Returns:
        //     A System.Data.Entity.Infrastructure.DbRawSqlQuery<TElement> object that will
        //     execute the query when it is enumerated.
        DbRawSqlQuery<TElement> SqlQuery<TElement>(string sql, params object[] parameters);
        //
        // Summary:
        //     Creates a raw SQL query that will return elements of the given type.  The
        //     type can be any type that has properties that match the names of the columns
        //     returned from the query, or can be a simple primitive type. The type does
        //     not have to be an entity type. The results of this query are never tracked
        //     by the context even if the type of object returned is an entity type. Use
        //     the System.Data.Entity.DbSet.SqlQuery(System.String,System.Object[]) method
        //     to return entities that are tracked by the context.  As with any API that
        //     accepts SQL it is important to parameterize any user input to protect against
        //     a SQL injection attack. You can include parameter place holders in the SQL
        //     query string and then supply parameter values as additional arguments. Any
        //     parameter values you supply will automatically be converted to a DbParameter.
        //      context.Database.SqlQuery(typeof(Post), "SELECT * FROM dbo.Posts WHERE Author
        //     = @p0", userSuppliedAuthor); Alternatively, you can also construct a DbParameter
        //     and supply it to SqlQuery. This allows you to use named parameters in the
        //     SQL query string.  context.Database.SqlQuery(typeof(Post), "SELECT * FROM
        //     dbo.Posts WHERE Author = @author", new SqlParameter("@author", userSuppliedAuthor));
        //
        // Parameters:
        //   elementType:
        //     The type of object returned by the query.
        //
        //   sql:
        //     The SQL query string.
        //
        //   parameters:
        //     The parameters to apply to the SQL query string.
        //
        // Returns:
        //     A System.Data.Entity.Infrastructure.DbRawSqlQuery object that will execute
        //     the query when it is enumerated.
        DbRawSqlQuery SqlQuery(Type elementType, string sql, params object[] parameters);

        IEnumerable<T> ExecuteStoredProcedure<T>(object storedProcedure);

        //karun code
        DbContext DbContext { get; }

    }
}