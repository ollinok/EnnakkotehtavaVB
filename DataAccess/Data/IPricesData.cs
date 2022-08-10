namespace DataAccess.Data;

public interface IPricesData
{
    Task CreatePrice(int articleId, PricesModel price);
    Task CreatePricesForArticle(List<FullPricesModel> prices);
    Task<IEnumerable<FullPricesModel>> GetAllPricesByArticleId(int id);
}
