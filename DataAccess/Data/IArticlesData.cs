namespace DataAccess.Data
{
    public interface IArticlesData
    {
        Task<ArticlesModel?> GetArticle(long id);
        Task<IEnumerable<ArticlesModel>> GetAllArticleNames();
        Task<IEnumerable<FullArticlesModel>> GetFullArticleInfo(long id);
        Task<string> CreateNewArticle(ArticlesModel article);
        Task DeleteArticle(long id);
    }
}
