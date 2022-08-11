namespace DataAccess.Models;
public class OrdersModel
{
    public long Id { get; set; }
    public long CustomerId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
