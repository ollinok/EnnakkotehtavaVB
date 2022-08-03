using DataAccess.Models;

namespace DataAccess.Data
{
    public interface IArticlesData
    {
        Task<ArticlesModel?> GetArticle(int id);
        Task<IEnumerable<ArticlesModel>> GetArticleNames();
    }
}