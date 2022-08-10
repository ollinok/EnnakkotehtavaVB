namespace DataAccess.Data;
public class PricesData : IPricesData
{
    private readonly ISqlAccess _db;

    public PricesData(ISqlAccess db)
    {
        _db = db;
    }

    /*public async Task<IEnumerable<PricesModel>> GetAllPricesByArticleId(int id)
    {
        string sql = @"select * from prices where article_id = @AId order by price_group_id";
        return await _db.LoadSqlData<PricesModel, dynamic>(sql, new { AId = id });
    }*/

    public async Task<IEnumerable<FullPricesModel>> GetAllPricesByArticleId(int id)
    {
        string sql = @"select prices.id, prices.price_group_id, prices.article_id, prices.price, prices.created_at CreatedAt, prices.updated_at UpdatedAt,
                              price_groups.id, price_groups.name
                       from prices
                       left join price_groups on price_groups.id = prices.price_group_id
                       where prices.article_id = @AId
                       order by prices.price_group_id";
        Func<FullPricesModel, PriceGroupsModel, FullPricesModel> func =
            (price, pGroup) => { price.PriceGroup = pGroup; return price; };
        string splitOn = "id";

        return await _db.LoadMultiMapSqlData<FullPricesModel, PriceGroupsModel, dynamic>(sql, new { AId = id }, func, splitOn);
    }

    public async Task CreatePrice(int articleId, PricesModel price)
    {
        string sql = @"insert into prices (price_group_id, article_id, price, created_at)
                       values (@PgID, @AId, @Price, CURRENT_TIMESTAMP)";
        var param = new
        {
            PgId = price.PriceGroupId,
            AId = articleId,
            Price = price.Price
        };

        await _db.WriteSqlData(sql, param);
    }

    public async Task CreatePricesForArticle(List<FullPricesModel> prices, int articleId)
    {
        string sql = $@"insert into prices (price_group_id, article_id, price, created_at)
                       values (@PriceGroupId, {articleId}, @Price, CURRENT_TIMESTAMP)";
        await _db.WriteSqlData(sql, prices);
    }
}
