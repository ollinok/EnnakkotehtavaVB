namespace DataAccess.Models.HelperModels;
public class FullArticlesModel : ArticlesModel
{
    public PricesModel? Price { get; set; }
    public PriceGroupsModel? PriceGroup { get; set; }
}
