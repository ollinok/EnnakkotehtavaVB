using DataAccess.DbAccess;
using DataAccess.Models;
using DataAccess.Models.HelperModels;

namespace DataAccess.Data;

public class ArticlesData : IArticlesData
{
    private readonly ISqlAccess _db;

    public ArticlesData(ISqlAccess db)
    {
        _db = db;
    }

    public Task<IEnumerable<ArticlesModel>> GetArticleNames()
    {
        string sqlProcedure = "select id, name from articles";
        return _db.LoadSqlData<ArticlesModel, dynamic>(sqlProcedure, new { });
    }

    public async Task<ArticlesModel?> GetArticle(int id)
    {
        string sqlProcedure = "select * from articles where id = @Id";
        var article = await _db.LoadSqlData<ArticlesModel, dynamic>(sqlProcedure, new { Id = id });
        return article.FirstOrDefault();
    }

    public Task<IEnumerable<FullArticlesModel>> GetFullArticleInfo(int id)
    {
        string sqlProcedure = @"select articles.created_at CreatedAt, articles.*, prices.price, prices.price_group_id PriceGroupId, prices.created_at CreatedAt 
                                from articles
                                left join prices on prices.article_id = articles.id
                                where articles.id = @Id";
        Func<FullArticlesModel, PricesModel, FullArticlesModel> func = (article, price) => { article.Price = price; return article; };
        string splitOn = "updated_at";

        return _db.LoadMultiMapSqlData<FullArticlesModel, PricesModel, dynamic>(sqlProcedure, new { Id = id }, func, splitOn);
    }
}
