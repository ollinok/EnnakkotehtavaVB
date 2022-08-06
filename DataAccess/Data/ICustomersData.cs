namespace DataAccess.Data;

public interface ICustomersData
{
    Task<IEnumerable<CustomersModel>> GetAllCustomerNames();
    Task<IEnumerable<FullCustomersModel>> GetFullCustomerInfo(int id);
}
