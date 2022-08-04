using DataAccess.Models.HelperModels;

namespace DataAccess.DbAccess
{
    public interface ISqlAccess
    {
        Task<IEnumerable<T1>> LoadMultiMapSqlData<T1, T2, U>(string sqlProcedure, U parameters, Func<T1, T2, T1> func, string splitCol, string connectionId = "default");
        Task<IEnumerable<T1>> LoadMultiMapSqlData<T1, T2, T3, U>(string sqlProcedure, U parameters, Func<T1, T2, T3, T1> func, string splitCol, string connectionId = "default");
        Task<IEnumerable<T>> LoadSqlData<T, U>(string sqlProcedure, U parameters, string connectionId = "Default");
        Task WriteSqlData<T>(string sqlProcedure, T parameters, string connectionId);
    }
}
