namespace DataAccess.Models.HelperModels;
public class FullCustomersModel : CustomersModel
{
    public PriceGroupsModel? PriceGroup { get; set; }
    public int TotalOrders { get; set; }
}
