namespace DataAccess.Data;
public class PricesData : IPricesData
{
    private readonly ISqlAccess _db;

    public PricesData(ISqlAccess db)
    {
        _db = db;
    }

    public async Task<IEnumerable<FullPricesModel>> GetAllPricesByArticleId(long id)
    {
        string sql = @"select prices.id, prices.price_group_id PriceGroupId, prices.article_id ArticleId, prices.price, prices.created_at CreatedAt, prices.updated_at UpdatedAt,
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

    public async Task CreatePricesForArticle(List<FullPricesModel> prices, long articleId)
    {
        string sql = $@"insert into prices (price_group_id, article_id, price, created_at)
                       values (@PriceGroupId, {articleId}, @Price, CURRENT_TIMESTAMP)";
        await _db.WriteSqlData(sql, prices);
    }

    public async Task UpdatePricesForArticle(List<FullPricesModel> prices)
    {
        string sql = @"INSERT INTO prices (id, price_group_id, article_id, price, created_at, updated_at)
                       VALUES (@Id, @PriceGroupId, @ArticleId, @Price, CURRENT_TIMESTAMP, null)
                       ON DUPLICATE KEY
                       UPDATE price = @Price, updated_at = CURRENT_TIMESTAMP";
        await _db.WriteSqlData(sql, prices);
    }
}
