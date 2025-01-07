using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gravity.Models.Books;
using System.Linq;
using System.Threading.Tasks;
using Gravity.Model.Books;
using Gravity.Models.ViewModels;
using Gravity.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace Gravity.Controllers
{
    [Route("authors")]
    public class AuthorController : Controller
    {
        private readonly BooksContext _context;

        // Konstruktor - inicjalizacja bazy danych
        public AuthorController(BooksContext context)
        {
            _context = context;
        }

        // Wyświetlenie listy autorów z opcją wyszukiwania
        [HttpGet("")]
        public async Task<IActionResult> Index(string searchAuthor, int? pageNumber)
        {
            const int pageSize = 20;
            ViewData["CurrentFilter"] = searchAuthor;

            IQueryable<Author> authorsQuery = _context.Authors.Include(a => a.Books);

            // Filtrowanie na poziomie bazy danych
            if (!string.IsNullOrEmpty(searchAuthor))
            {
                authorsQuery = authorsQuery.Where(a => EF.Functions.Like(a.AuthorName, $"{searchAuthor}%"));
            }

            var paginatedAuthors = await PaginatedList<AuthorViewModel>.CreateAsync(
                authorsQuery.Select(a => new AuthorViewModel
                {
                    AuthorId = a.AuthorId,
                    AuthorName = a.AuthorName,
                    Books = a.Books.Select(b => new BookViewModel
                    {
                        BookId = b.BookId,
                        Title = b.Title
                    }).ToList()
                }), pageNumber ?? 1, pageSize);

            // Jeśli jest zapytanie AJAX, zwróć tylko widok częściowy
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("_AuthorsTable", paginatedAuthors);
            }

            return View(paginatedAuthors);
        }


        // Formularz dodawania nowego autora
        [HttpGet("create")]
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // Obsługa dodawania nowego autora
        [HttpPost("create")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AuthorName")] Author author)
        {
            if (ModelState.IsValid)
            {
                _context.Add(author); // Dodanie autora
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index)); // Powrót do listy autorów
            }
            return View(author);
        }

        // Wyświetlenie autorów dla danej książki
        [HttpGet("authors-by-book/{bookId}")]
        public async Task<IActionResult> AuthorsByBook(int bookId)
        {
            // Pobranie autorów powiązanych z książką
            var authors = await _context.Authors
                .Include(a => a.Books)
                .Where(a => a.Books.Any(b => b.BookId == bookId))
                .ToListAsync();

            if (authors == null || !authors.Any())
            {
                return NotFound($"Brak autorów dla książki o ID {bookId}.");
            }

            ViewData["BookId"] = bookId;
            return View(authors);
        }
    }
}
