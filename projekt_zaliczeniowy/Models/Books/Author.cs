using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gravity.Models.Books
{
    public partial class Author
    {
       
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Dodaj ten atrybut
        public int AuthorId { get; set; }

        [Required(ErrorMessage = "Nazwa autora jest wymagana.")]
        [StringLength(100, ErrorMessage = "Nazwa autora nie może przekraczać 100 znaków.")]
        [Column("author_name")]
        public string AuthorName { get; set; }

        // Nawigacja do Book (wiele-do-wielu)!!!
        public virtual ICollection<Book> Books { get; set; } = new List<Book>();
    }
}