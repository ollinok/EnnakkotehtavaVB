namespace DataAccess.Data;

public interface IOrdersData
{
    /*Task<IEnumerable<FullOrdersModel>> GetAllOrdersByCustomer(int id);*/
    Task<List<FullOrdersModel>> GetOrdersByList(IEnumerable<OrdersModel> orders);
}
