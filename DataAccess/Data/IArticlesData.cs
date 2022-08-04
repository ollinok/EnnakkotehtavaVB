namespace DataAccess.Data
{
    public interface IArticlesData
    {
        Task<ArticlesModel?> GetArticle(int id);
        Task<IEnumerable<ArticlesModel>> GetArticleNames();
        Task<IEnumerable<FullArticlesModel>> GetFullArticleInfo(int id);
    }
}
