namespace DataAccess.Data;

public interface IOrdersData
{
    Task<List<FullOrdersModel>?> GetOrdersByList(IEnumerable<OrdersModel> orders);
}
