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

    public async Task<IEnumerable<FullCustomersModel>> GetFullCustomerInfo(int id)
    {
        string sqlProcedure = @"select customers.id, customers.name, customers.email, customers.address, customers.created_at CreatedAt, customers.updated_at UpdatedAt,
                                       price_groups.name,
                                       orders.id, orders.created_at CreatedAt, orders.updated_at UpdatedAt
                                from customers
                                left join price_groups on price_groups.id = customers.price_group_id
                                left join orders on orders.customer_id = customers.id
                                where customers.id = @Id";
        Func<FullCustomersModel, PriceGroupsModel, OrdersModel, FullCustomersModel> func
            = (customer, priceGroup, orders) => { customer.PriceGroup = priceGroup; customer.Orders = orders; return customer; };
        string splitOn = "name,id";

        return await _db.LoadMultiMapSqlData<FullCustomersModel, PriceGroupsModel, OrdersModel, dynamic>(sqlProcedure, new { Id = id }, func, splitOn);
    }
}
