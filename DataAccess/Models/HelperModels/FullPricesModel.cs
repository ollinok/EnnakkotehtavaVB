namespace DataAccess.Models.HelperModels;
public class FullPricesModel : PricesModel
{
    public PriceGroupsModel? PriceGroup { get; set; }

    public FullPricesModel()
    {

    }
    public FullPricesModel(FullPricesModel fPrices)
    {
        Id = fPrices.Id;
        PriceGroupId = fPrices.PriceGroupId;
        ArticleId = fPrices.ArticleId;
        Price = fPrices.Price;
        CreatedAt = fPrices.CreatedAt;
        UpdatedAt = fPrices.UpdatedAt;
        PriceGroup = fPrices.PriceGroup;
    }
}
