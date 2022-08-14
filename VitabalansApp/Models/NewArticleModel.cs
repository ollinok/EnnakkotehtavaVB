using System.ComponentModel.DataAnnotations;

namespace VitabalansApp.Models;
public class NewArticleModel
{
    [Required(ErrorMessage = "Anna nimi.")]
    [MinLength(4, ErrorMessage = "Nimen on oltava vähintään 4 merkkiä.")]
    public string? Title { get; set; }
    [Required]
    [Range(1, 2000, ErrorMessage = "Annoksen määrä ei voi olla negatiivinen, 0mg tai yli 2000mg.")]
    public int Mg { get; set; }
    [Required]
    [Range(1, 1000, ErrorMessage = "Pakkauksen määrä ei voi olla negatiivinen, 0 tai yli 1000.")]
    public int Amount { get; set; }
    [Required(ErrorMessage = "Anna EAN-13-standardin mukainen koodi.")]
    [RegularExpression(@"^\d{13}$", ErrorMessage = "Koodin täytyy olla 13 merkkiä pitkä sisältäen vain numeroita.")]
    public string? Ean { get; set; }
}
