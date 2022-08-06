namespace DataAccess.Data;

public interface IOrdersData
{
    Task<IEnumerable<FullOrdersModel>> GetAllOrdersByCustomerId(int id);
}