using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models;

public class ArticlesModel
{
    public int Id { get; set; }
    [RegularExpression(@"^[A-Z][a-z]{3,}\s\d+\smg\s\d+\stabl.$")]
    public string? Name { get; set; }
    public string? Ean { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
