using Microsoft.Extensions.Caching.Memory;

namespace DataAccess.Data;
public class ArticlesData : IArticlesData
{
    private readonly ISqlAccess _db;
    private readonly IMemoryCache _cache;
    private const string cacheName = "ArticlesCache";

    public ArticlesData(ISqlAccess db, IMemoryCache cache)
    {
        _db = db;
        _cache = cache;
    }

    public async Task<IEnumerable<ArticlesModel>> GetAllArticleNames()
    {
        var results = _cache.Get<IEnumerable<ArticlesModel>>(cacheName);
        if (results == null)
        {
            string sqlProcedure = "select id, name from articles order by name";
            results = await _db.LoadSqlData<ArticlesModel, dynamic>(sqlProcedure, new { });

            _cache.Set(cacheName, results, TimeSpan.FromMinutes(1));
        }
        return results;
    }

    public async Task<ArticlesModel?> GetArticle(int id)
    {
        string sqlProcedure = "select * from articles where id = @Id";
        var article = await _db.LoadSqlData<ArticlesModel, dynamic>(sqlProcedure, new { Id = id });
        return article.FirstOrDefault();
    }

    public async Task<ArticlesModel> CreateNewArticle()
    {
        return null;
    }

    public async Task<IEnumerable<FullArticlesModel>> GetFullArticleInfo(int id)
    {
        string sqlProcedure = @"select articles.id, articles.name, articles.ean, articles.created_at CreatedAt, articles.updated_at UpdatedAt,
                                       prices.price, prices.created_at CreatedAt, prices.updated_at UpdatedAt,
                                       price_groups.name
                                from articles
                                left join prices on prices.article_id = articles.id
                                left join price_groups on price_groups.id = prices.price_group_id
                                where articles.id = @Id";
        Func<FullArticlesModel, PricesModel, PriceGroupsModel, FullArticlesModel> func 
            = (article, price, priceGroup) => { article.Price = price; article.PriceGroup = priceGroup; return article; };
        string splitOn = "price,name";

        return await _db.LoadMultiMapSqlData<FullArticlesModel, PricesModel, PriceGroupsModel, dynamic>(sqlProcedure, new { Id = id }, func, splitOn);
    }
}
