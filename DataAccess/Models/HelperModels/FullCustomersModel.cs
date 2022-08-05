namespace DataAccess.Models.HelperModels;
public class FullCustomersModel : CustomersModel
{
    public PriceGroupsModel? PriceGroup { get; set; }
    public FullOrdersModel? Orders { get; set; }
}
