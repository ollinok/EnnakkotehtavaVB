using Microsoft.Extensions.Caching.Memory;

namespace DataAccess.Data;
public class OrdersData : IOrdersData
{
    private readonly ISqlAccess _db;

    public OrdersData(ISqlAccess db)
    {
        _db = db;
    }

    public async Task<IEnumerable<FullOrdersModel>> GetAllOrdersByCustomerId(int id)
    {
        string sql = @"select id, created_at CreatedAt, updated_at UpdatedAt
                       from orders
                       where customer_id = @Id;
                       select order_items.id, order_items.quantity, order_items.price,
                              articles.name
                       from order_items
                       left join order_items on order_items.order_id = orders.id
                       left join articles on articles.id = order_items.article_id
                       where orders.customer_id = @Id";
        Func<FullOrdersModel, OrderItemsModel, ArticlesModel, FullOrdersModel> func =
            (order, orderI, article) => { return order; };
        string splitOn = "id,name";

        return await _db.LoadMultiMapSqlData<FullOrdersModel, OrderItemsModel, ArticlesModel, dynamic>(sql, new { Id = id }, func, splitOn);
    }
}
