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

            _cache.Set(cacheName, results, TimeSpan.FromMinutes(1));
        }
        return results;
    }

    public async Task<FullCustomersModel?> GetCustomerInfo(int id)
    {
        string sql = @"select customers.id, customers.name, customers.email, customers.address, customers.created_at CreatedAt, customers.updated_at UpdatedAt,
                              price_groups.name,
                              count(orders.id) as TotalOrders
                       from customers
                       left join price_groups on price_groups.id = customers.price_group_id
                       left join orders on orders.customer_id = customers.id
                       where customers.id = @Id";
        Func<FullCustomersModel, PriceGroupsModel, long, FullCustomersModel> func
            = (customer, priceGroup, orders) => { customer.PriceGroup = priceGroup; customer.TotalOrders = (int)orders; return customer; };
        string splitOn = "name,TotalOrders";

        return (await _db.LoadMultiMapSqlData<FullCustomersModel, PriceGroupsModel, long, dynamic>(sql, new { Id = id }, func, splitOn)).FirstOrDefault();
    }
}
