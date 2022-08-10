using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models;
public class PricesModel
{
    public int Id { get; set; }
    public int PriceGroupId { get; set; }
    public int ArticleId { get; set; }
    [Required]
    [Range(0, 10000, ErrorMessage = "Hinta ei voi olla negatiivinen luku.")]
    public decimal Price { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
