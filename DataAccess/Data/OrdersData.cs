using Microsoft.Extensions.Caching.Memory;

namespace DataAccess.Data;
public class OrdersData : IOrdersData
{
    private readonly ISqlAccess _db;

    public OrdersData(ISqlAccess db)
    {
        _db = db;
    }

    /*public async Task<FullOrdersModel> GetOrderById(int id)
    {
        string sql = @"select order_items.id, order_items.quantity, order_items.price, order_items.created_at CreatedAt, order_items.updated_at UpdatedAt,
                              articles.id, articles.name, articles.ean
                       from order_items
                       left join articles on articles.id = order_items.article_id
                       where order_items.order_id = @Id";
        Func<FullOrderItemsModel, ArticlesModel, FullOrderItemsModel> func =
            (orderI, article) => { orderI.Article = article; return orderI; };
        string splitOn = "id";

        IEnumerable<FullOrderItemsModel> list = await _db.LoadMultiMapSqlData<FullOrderItemsModel, ArticlesModel, dynamic>(sql, new { Id = id }, func, splitOn);
        return;
    }*/

    public async Task<List<FullOrdersModel>> GetOrdersByList(IEnumerable<OrdersModel> orders)
    {
        string sql = "";
        foreach (var order in orders)
        {
            sql += $@"select id, customer_id, created_at CreatedAt, updated_at UpdatedAt
                      from orders
                      where id = {order.Id};
                      select order_items.id, order_items.quantity, order_items.price, order_items.created_at CreatedAt, order_items.updated_at UpdatedAt,
                             articles.id, articles.name, articles.ean
                      from order_items
                      left join articles on articles.id = order_items.article_id
                      where order_items.order_id = {order.Id};";
        }
        string splitOn = "id";

        return await _db.LoadManyOrderDetails(sql, new { }, splitOn);
    }
}
