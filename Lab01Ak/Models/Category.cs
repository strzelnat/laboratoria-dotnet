using System.ComponentModel.DataAnnotations;

namespace Lab01Ak.Models;

public enum Category
{
    [Display(Name = "Rodzina")]
    Family = 1,
    [Display(Name = "Znajomy")]
    Friend = 2,
    [Display(Name = "Kontakt Zawodowy")]
    Business = 4
}