namespace DataAccess.Data;
public class DatabaseData : IDatabaseData
{
    private readonly ISqlAccess _db;

    public DatabaseData(ISqlAccess db)
    {
        _db = db;
    }

    public async Task<DbRowCountModel?> GetTableCounts()
    {
        string sql = @"SELECT COUNT(*) as ArticleCount FROM articles;
                       SELECT COUNT(*) as OrderCount FROM orders;
                       SELECT COUNT(*) as CustomerCount FROM customers;";
        return await _db.LoadDatabaseCount(sql, new DbRowCountModel());
    }
}
