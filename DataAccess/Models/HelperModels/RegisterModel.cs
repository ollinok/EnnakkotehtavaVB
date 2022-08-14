using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models;

public class RegisterModel : UsersModel
{
    [Required]
    [DataType(DataType.Password)]
    [Compare("Password")]
    public string? ConfirmPassword { get; set; }
}
