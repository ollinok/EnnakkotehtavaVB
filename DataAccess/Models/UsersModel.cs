using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models;
public class UsersModel
{
    public long Id { get; set; }
    [Required]
    public string? Name { get; set; }
    [Required]
    [EmailAddress]
    public string? Email { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string? Password { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
