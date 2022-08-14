namespace DataAccess.Data;

public interface IPricesData
{
    Task CreatePricesForArticle(List<FullPricesModel> prices, long articleId);
    Task<IEnumerable<FullPricesModel>> GetAllPricesByArticleId(long id);
    Task UpdatePricesForArticle(List<FullPricesModel> prices);
}
