namespace DataAccess.DbAccess
{
    public interface ISqlAccess
    {
        Task<IEnumerable<T>> LoadSqlData<T, U>(string sqlProcedure, U parameters, string connectionId = "Default");
        Task WriteSqlData<T>(string sqlProcedure, T parameters, string connectionId);
    }
}