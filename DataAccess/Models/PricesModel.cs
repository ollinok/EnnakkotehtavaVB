using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models;
public class PricesModel
{
    public long Id { get; set; }
    public long PriceGroupId { get; set; }
    public long ArticleId { get; set; }
    [Required]
    [Range(0, 10000, ErrorMessage = "Hinta ei voi olla negatiivinen luku.")]
    public decimal Price { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
