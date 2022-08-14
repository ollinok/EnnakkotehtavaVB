using Microsoft.Extensions.Caching.Memory;

namespace DataAccess.Data;
public class DatabaseData : IDatabaseData
{
    private readonly ISqlAccess _db;
    private readonly IMemoryCache _cache;
    private const string cacheName = "CountsCache";

    public DatabaseData(ISqlAccess db, IMemoryCache cache)
    {
        _db = db;
        _cache = cache;
    }

    public async Task<DbRowCountModel?> GetTableCounts()
    {
        var results = _cache.Get<DbRowCountModel>(cacheName);
        if (results == null)
        {
            string sql = @"SELECT COUNT(*) as ArticleCount FROM articles;
                       SELECT COUNT(*) as OrderCount FROM orders;
                       SELECT COUNT(*) as CustomerCount FROM customers;";
            results = await _db.LoadDatabaseCount(sql, new DbRowCountModel());

            _cache.Set(cacheName, results, TimeSpan.FromMinutes(1));
        }

        return results;
    }
}
