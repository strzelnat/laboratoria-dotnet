using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gravity.Models.Books
{
    public partial class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookId { get; set; }
        
     
        [Required(ErrorMessage = "Tytuł jest wymagany.")]
        [StringLength(200, ErrorMessage = "Tytuł nie może przekraczać 200 znaków.")]
        public string Title { get; set; }

       
        [Required(ErrorMessage = "ISBN13 jest wymagany.")]
        [StringLength(13, MinimumLength = 13, ErrorMessage = "ISBN13 musi mieć dokładnie 13 znaków.")]
        public string Isbn13 { get; set; }

        public int? LanguageId { get; set; }

       
        [Range(1, int.MaxValue, ErrorMessage = "Liczba stron musi być większa niż 0.")]
        public int NumPages { get; set; }

       
        [DataType(DataType.Date)]
        public DateTime? PublicationDate { get; set; }

        public int? PublisherId { get; set; }

        // Nawigacja do BookLanguage
        public virtual BookLanguage Language { get; set; }

        // Nawigacja do Publisher
        public virtual Publisher Publisher { get; set; }

        // Nawigacja do OrderLines
        public virtual ICollection<OrderLine> OrderLines { get; set; } = new List<OrderLine>();

        // Nawigacja do Author (wiele-do-wielu)
       
        public virtual ICollection<Author> Authors { get; set; } = new List<Author>();
        
        
        
    }
}