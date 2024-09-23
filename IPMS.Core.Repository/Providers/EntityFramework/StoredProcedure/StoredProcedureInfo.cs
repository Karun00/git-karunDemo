using System.Data.SqlClient;

namespace Core.Repository.Providers.EntityFramework
{
    internal class StoredProcedureInfo
    {       
        internal string Sql { get; set; }
        internal SqlParameter[] SqlParameters { get; set; }
    }

}
