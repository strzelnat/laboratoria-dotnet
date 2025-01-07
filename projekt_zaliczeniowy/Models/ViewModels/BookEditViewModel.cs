using System;
using System.ComponentModel.DataAnnotations;

namespace Gravity.Models.ViewModels
{
    public class BookEditViewModel
    {
        public int BookId { get; set; }

        [Required(ErrorMessage = "Tytuł jest wymagany.")]
        [StringLength(200, ErrorMessage = "Tytuł nie może przekraczać 200 znaków.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "ISBN-13 jest wymagany.")]
        [StringLength(13, MinimumLength = 13, ErrorMessage = "ISBN-13 musi mieć dokładnie 13 znaków.")]
        public string Isbn13 { get; set; }

        [Required(ErrorMessage = "Liczba stron jest wymagana.")]
        [Range(1, int.MaxValue, ErrorMessage = "Liczba stron musi być większa niż 0.")]
        public int NumPages { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Data publikacji jest wymagana.")]
        public DateTime? PublicationDate { get; set; }
    }
}