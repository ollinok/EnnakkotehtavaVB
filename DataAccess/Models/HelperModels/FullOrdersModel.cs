namespace DataAccess.Models.HelperModels;
public class FullOrdersModel : OrdersModel
{
    public IEnumerable<FullOrderItemsModel>? OrderItems { get; set; }
}
