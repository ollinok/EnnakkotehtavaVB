using Microsoft.Extensions.Caching.Memory;

namespace DataAccess.Data;
public class CustomersData : ICustomersData
{
    private readonly ISqlAccess _db;
    private readonly IMemoryCache _cache;
    private const string cacheName = "CustomersCache";

    public CustomersData(ISqlAccess db, IMemoryCache cache)
    {
        _db = db;
        _cache = cache;
    }

    public async Task<IEnumerable<CustomersModel>> GetAllCustomerNames()
    {
        var results = _cache.Get<IEnumerable<CustomersModel>>(cacheName);
        if (results == null)
        {
            string sqlProcedure = "select id, name from customers order by name";
            results = await _db.LoadSqlData<CustomersModel, dynamic>(sqlProcedure, new { });

            _cache.Set(cacheName, results, TimeSpan.FromMinutes(5));
        }
        return results;
    }
}
