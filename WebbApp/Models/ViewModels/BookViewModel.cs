using System;

namespace Gravity.Models.ViewModels
{
    public class BookViewModel
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Isbn13 { get; set; }
        public int NumPages { get; set; }
        public DateTime? PublicationDate { get; set; }
        public int AuthorsCount { get; set; }
        public int CopiesSold { get; set; }
    }
}