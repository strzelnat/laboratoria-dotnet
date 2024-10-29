using System.ComponentModel.DataAnnotations;

namespace laboratorium1.Models
{
    public enum Category
    {
        [Display(Name ="rodzina")]
        Family = 1,
        [Display(Name = "przyjaciel")]
        Friend = 2,
        [Display(Name = "praca")]
        Business = 4,

    }
}
