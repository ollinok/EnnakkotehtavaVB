namespace DataAccess.Models;

public class OrderItemsModel
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int ArticleId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
