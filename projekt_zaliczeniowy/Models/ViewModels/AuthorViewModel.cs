using System.Collections.Generic;

namespace Gravity.Models.ViewModels
{
    public class AuthorViewModel
    {
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public List<BookViewModel> Books { get; set; } = new List<BookViewModel>();
    }
}