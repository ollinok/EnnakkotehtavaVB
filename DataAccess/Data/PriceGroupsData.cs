using Microsoft.Extensions.Caching.Memory;

namespace DataAccess.Data;
public class PriceGroupsData : IPriceGroupsData
{
    private readonly ISqlAccess _db;
    private readonly IMemoryCache _cache;
    private const string cacheName = "PriceGroupCache";

    public PriceGroupsData(ISqlAccess db, IMemoryCache cache)
    {
        _db = db;
        _cache = cache;
    }

    public async Task<IEnumerable<PriceGroupsModel>> GetAllPriceGroups()
    {
        var results = _cache.Get<IEnumerable<PriceGroupsModel>>(cacheName);
        if (results == null)
        {
            string sql = @"select * from price_groups";
            results = await _db.LoadSqlData<PriceGroupsModel, dynamic>(sql, new { });

            _cache.Set(cacheName, results, TimeSpan.FromMinutes(1));
        }
        return results;
    }
}
