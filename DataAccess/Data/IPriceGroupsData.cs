namespace DataAccess.Data;

public interface IPriceGroupsData
{
    Task<IEnumerable<PriceGroupsModel>> GetAllPriceGroups();
}