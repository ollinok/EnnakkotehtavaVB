namespace DataAccess.Data;

public interface IPricesData
{
    Task CreatePrice(int articleId, PricesModel price);
    Task<IEnumerable<FullPricesModel>> GetAllPricesByArticleId(int id);
}
