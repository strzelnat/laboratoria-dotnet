using Microsoft.Extensions.Logging;
using Gravity.Models.Books;
using Gravity.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Gravity.Model.Books;
using Gravity.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace Gravity.Controllers
{
    [Route("books")]
    public class BooksController : Controller
    {
        private readonly BooksContext _context;
        private readonly ILogger<BooksController> _logger;

        public BooksController(BooksContext context, ILogger<BooksController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index(string searchTitle, int? pageNumber)
        {
            const int pageSize = 20;
            ViewData["CurrentFilter"] = searchTitle;

            var booksQuery = _context.Books
                .Include(b => b.Authors)
                .Include(b => b.OrderLines)
                .Select(b => new BookViewModel
                {
                    BookId = b.BookId,
                    Title = b.Title,
                    Isbn13 = b.Isbn13,
                    NumPages = b.NumPages,
                    PublicationDate = b.PublicationDate,
                    AuthorsCount = b.Authors.Count(),
                    CopiesSold = b.OrderLines.Count()
                });

            if (!string.IsNullOrEmpty(searchTitle))
            {
                booksQuery = booksQuery.Where(b => b.Title.StartsWith(searchTitle));
            }

            var paginatedBooks = await PaginatedList<BookViewModel>.CreateAsync(booksQuery, pageNumber ?? 1, pageSize);

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("_BooksTable", paginatedBooks);
            }

            return View(paginatedBooks);
        }

        [HttpGet("create")]
        [Authorize]
        public async Task<IActionResult> Create(string returnUrl = null)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account", new { returnUrl = Url.Action("Create", "Books") });
            }

            var authors = await _context.Authors.ToListAsync();
            var viewModel = new BookCreateViewModel
            {
                AuthorsList = authors.Select(a => new SelectListItem
                {
                    Value = a.AuthorId.ToString(),
                    Text = a.AuthorName
                }).ToList()
            };
            return View(viewModel);
        }
        
        [HttpGet("Edit")]
        [Authorize]
        public async Task<IActionResult> Edit(int bookId)
        {
            var book = await _context.Books.FindAsync(bookId);
            if (book == null)
            {
                return NotFound();
            }

            var viewModel = new BookEditViewModel
            {
                BookId = book.BookId,
                Title = book.Title,
                Isbn13 = book.Isbn13,
                NumPages = book.NumPages,
                PublicationDate = book.PublicationDate
            };

            return View(viewModel);
        }


        [HttpPost("Edit")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BookEditViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var book = await _context.Books.FindAsync(viewModel.BookId);
                if (book == null)
                {
                    return NotFound();
                }

                // Aktualizacja pól książki
                book.Title = viewModel.Title;
                book.Isbn13 = viewModel.Isbn13;
                book.NumPages = viewModel.NumPages;
                book.PublicationDate = viewModel.PublicationDate;

                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError("", $"Błąd zapisu: {ex.InnerException?.Message ?? ex.Message}");
                }
            }

            return View(viewModel);
        }

        

        [HttpPost("create")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookCreateViewModel viewModel, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var book = new Book
                {
                    Title = viewModel.Title,
                    Isbn13 = viewModel.Isbn13,
                    NumPages = viewModel.NumPages,
                    PublicationDate = viewModel.PublicationDate
                };

                if (viewModel.SelectedAuthorIds != null && viewModel.SelectedAuthorIds.Any())
                {
                    var distinctAuthorIds = viewModel.SelectedAuthorIds.Distinct().ToList();
                    var selectedAuthors = await _context.Authors
                        .Where(a => distinctAuthorIds.Contains(a.AuthorId))
                        .ToListAsync();

                    foreach (var author in selectedAuthors)
                    {
                        if (!book.Authors.Any(a => a.AuthorId == author.AuthorId))
                        {
                            book.Authors.Add(author);
                        }
                    }
                }

                _context.Books.Add(book);

                try
                {
                    await _context.SaveChangesAsync();
                    _logger.LogInformation("Dodano książkę {Title}", book.Title);
                    return Redirect(returnUrl ?? Url.Action("Index"));
                }
                catch (DbUpdateException ex)
                {
                    _logger.LogError(ex, "Błąd dodania książki");
                    ModelState.AddModelError("", $"Błąd zapisu: {ex.InnerException?.Message ?? ex.Message}");
                }
            }

            var authors = await _context.Authors.ToListAsync();
            viewModel.AuthorsList = authors.Select(a => new SelectListItem
            {
                Value = a.AuthorId.ToString(),
                Text = a.AuthorName
            }).ToList();

            return View(viewModel);
        }
    }
}