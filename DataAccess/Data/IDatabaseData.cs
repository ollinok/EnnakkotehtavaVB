namespace DataAccess.Data;

public interface IDatabaseData
{
    Task<DbRowCountModel?> GetTableCounts();
}