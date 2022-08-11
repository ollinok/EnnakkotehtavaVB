namespace DataAccess.Models;

public class OrderItemsModel
{
    public long Id { get; set; }
    public long OrderId { get; set; }
    public long ArticleId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
