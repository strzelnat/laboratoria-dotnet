using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Gravity.Models.ViewModels
{
    public class BookCreateViewModel
    {
        [Display(Name = "Tytuł")]
        [Required(ErrorMessage = "Tytuł jest wymagany.")]
        [StringLength(90, MinimumLength = 5, ErrorMessage = "Tytuł musi mieć od 5 do 90 znaków.")]
        public string Title { get; set; }

        [Display(Name = "Numer Isbn13")]
        [Required(ErrorMessage = "ISBN13 jest wymagany.")]
        [StringLength(13, MinimumLength = 13, ErrorMessage = "ISBN13 musi mieć dokładnie 13 znaków.")]
        public string Isbn13 { get; set; }

        [Display(Name = "Liczba stron")]
        [Range(5, 5000, ErrorMessage = "Liczba stron musi być pomiędzy 5 a 5000.")]
        public int NumPages { get; set; }

        [Display(Name = "Data publikacji")]
        [DataType(DataType.Date)]
        public DateTime? PublicationDate { get; set; }

        [Display(Name = "Autorzy")]
        public List<SelectListItem> AuthorsList { get; set; }

        [Required(ErrorMessage = "Proszę wybrać przynajmniej jednego autora.")]
        public List<int> SelectedAuthorIds { get; set; }

        public BookCreateViewModel()
        {
            SelectedAuthorIds = new List<int>();
            AuthorsList = new List<SelectListItem>();
        }
    }
}
