using DataAccess.DbAccess;
using DataAccess.Models;

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
        string sqlProcedure = "select id, name from articles limit 20";
        return _db.LoadSqlData<ArticlesModel, dynamic>(sqlProcedure, new { });
    }

    public async Task<ArticlesModel?> GetArticle(int id)
    {
        string sqlProcedure = "select * from articles where id = @Id";
        var article = await _db.LoadSqlData<ArticlesModel, dynamic>(sqlProcedure, new { Id = id });
        return article.FirstOrDefault();
    }
}
