namespace DataAccess.Data
{
    public interface IArticlesData
    {
        Task<ArticlesModel?> GetArticle(int id);
        Task<IEnumerable<ArticlesModel>> GetAllArticleNames();
        Task<IEnumerable<FullArticlesModel>> GetFullArticleInfo(int id);
        Task<string> CreateNewArticle(ArticlesModel article);
    }
}
