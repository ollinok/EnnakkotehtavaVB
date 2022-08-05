namespace DataAccess.Data;

public interface ICustomersData
{
    Task<IEnumerable<CustomersModel>> GetAllCustomerNames();
}
