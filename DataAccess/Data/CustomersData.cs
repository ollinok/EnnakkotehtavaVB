using Microsoft.Extensions.Caching.Memory;

namespace DataAccess.Data;
public class CustomersData : ICustomersData
{
    private readonly ISqlAccess _db;
    private readonly IMemoryCache _cache;
    private const string CacheName = "CustomersCache";

    public CustomersData(ISqlAccess db, IMemoryCache cache)
    {
        _db = db;
        _cache = cache;
    }

    public async Task<IEnumerable<CustomersModel>> GetAllCustomerNames()
    {
        var results = _cache.Get<IEnumerable<CustomersModel>>(CacheName);
        if (results == null)
        {
            string sqlProcedure = "select id, name from customers order by name";
            results = await _db.LoadSqlData<CustomersModel, dynamic>(sqlProcedure, new { });

            _cache.Set(CacheName, results, TimeSpan.FromMinutes(1));
        }
        return results;
    }

    public async Task<FullCustomersModel?> GetCustomerInfo(long id)
    {
        string sql = @"select customers.id, customers.name, customers.email, customers.address, customers.created_at CreatedAt, customers.updated_at UpdatedAt,
                              price_groups.name
                       from customers
                       left join price_groups on price_groups.id = customers.price_group_id
                       where customers.id = @Id;
                       select id, created_at CreatedAt, updated_at UpdatedAt from orders where customer_id = @Id";
        string splitOn = "name";

        return await _db.LoadCustomerInfo(sql, new { Id = id }, splitOn);
    }
}
