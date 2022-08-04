using DataAccess.DbAccess;
using DataAccess.Models;
using DataAccess.Models.HelperModels;
using Microsoft.Extensions.Caching.Memory;

namespace DataAccess.Data;

public class ArticlesData : IArticlesData
{
    private readonly ISqlAccess _db;
    private readonly IMemoryCache _cache;
    private const string cacheName = "ArticleCache";

    public ArticlesData(ISqlAccess db, IMemoryCache cache)
    {
        _db = db;
        _cache = cache;
    }

    public async Task<IEnumerable<ArticlesModel>> GetArticleNames()
    {
        var results = _cache.Get<IEnumerable<ArticlesModel>>(cacheName);
        if (results == null)
        {
            string sqlProcedure = "select id, name from articles";
            results = await _db.LoadSqlData<ArticlesModel, dynamic>(sqlProcedure, new { });

            _cache.Set(cacheName, results, TimeSpan.FromMinutes(5));
        }
        return results;
    }

    public async Task<ArticlesModel?> GetArticle(int id)
    {
        string sqlProcedure = "select * from articles where id = @Id";
        var article = await _db.LoadSqlData<ArticlesModel, dynamic>(sqlProcedure, new { Id = id });
        return article.FirstOrDefault();
    }

    /*public Task<IEnumerable<FullArticlesModel>> GetFullArticleInfo(int id)
    {
        string sqlProcedure = @"select articles.created_at CreatedAt, articles.*, prices.price, prices.price_group_id PriceGroupId, prices.created_at CreatedAt 
                                from articles
                                left join prices on prices.article_id = articles.id
                                where articles.id = @Id";
        Func<FullArticlesModel, PricesModel, FullArticlesModel> func = (article, price) => { article.Price = price; return article; };
        string splitOn = "updated_at";

        return _db.LoadMultiMapSqlData<FullArticlesModel, PricesModel, dynamic>(sqlProcedure, new { Id = id }, func, splitOn);
    }*/

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
