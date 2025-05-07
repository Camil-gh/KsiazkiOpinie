using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KsiazkiOpinie.Data;
using KsiazkiOpinie.Models;
using KsiazkiOpinie.ViewModels;

namespace KsiazkiOpinie.Controllers
{
    public class BooksController : Controller
    {
        private readonly BookAppContext _context;

        public BooksController(BookAppContext context)
        {
            _context = context;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            var bookAppContext = _context.Books
                
                .Include(b => b.User)
                .Include(b => b.AuthorBooks).ThenInclude(ab => ab.Author)
                .Include(b => b.CategoryBooks).ThenInclude(cb => cb.Category);

            return View(await bookAppContext.ToListAsync());
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            var viewModel = new BookCreateViewModel
            {
                Authors = new MultiSelectList(_context.Authors, "Id", "Name"),
                Categories = new MultiSelectList(_context.Categories, "Id", "Name"),
                Users = new SelectList(_context.Users, "Id", "Name")
            };

            return View(viewModel);
        }

        // POST: Books/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Books.Add(viewModel.Book);
                await _context.SaveChangesAsync();

                foreach (var authorId in viewModel.SelectedAuthorIds)
                {
                    _context.AuthorBooks.Add(new AuthorBook { AuthorId = authorId, BookId = viewModel.Book.Id });
                }

                foreach (var categoryId in viewModel.SelectedCategoryIds)
                {
                    _context.CategoryBooks.Add(new CategoryBook { CategoryId = categoryId, BookId = viewModel.Book.Id });
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            viewModel.Authors = new MultiSelectList(_context.Authors, "Id", "Name", viewModel.SelectedAuthorIds);
            viewModel.Categories = new MultiSelectList(_context.Categories, "Id", "Name", viewModel.SelectedCategoryIds);
            viewModel.Users = new SelectList(_context.Users, "Id", "Name", viewModel.Book.UserId);

            return View(viewModel);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var book = await _context.Books
                .Include(b => b.AuthorBooks)
                .Include(b => b.CategoryBooks)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (book == null) return NotFound();

            var viewModel = new BookCreateViewModel
            {
                Book = book,
                SelectedAuthorIds = book.AuthorBooks.Select(ab => ab.AuthorId).ToList(),
                SelectedCategoryIds = book.CategoryBooks.Select(cb => cb.CategoryId).ToList(),
                Authors = new MultiSelectList(_context.Authors, "Id", "Name"),
                Categories = new MultiSelectList(_context.Categories, "Id", "Name"),
                Users = new SelectList(_context.Users, "Id", "Name", book.UserId)
            };

            return View(viewModel);
        }

        // POST: Books/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BookCreateViewModel viewModel)
        {
            if (id != viewModel.Book.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(viewModel.Book);

                    // Usuń istniejące powiązania
                    var existingAuthorBooks = _context.AuthorBooks.Where(ab => ab.BookId == id);
                    _context.AuthorBooks.RemoveRange(existingAuthorBooks);

                    var existingCategoryBooks = _context.CategoryBooks.Where(cb => cb.BookId == id);
                    _context.CategoryBooks.RemoveRange(existingCategoryBooks);

                    // Dodaj nowe powiązania
                    foreach (var authorId in viewModel.SelectedAuthorIds)
                    {
                        _context.AuthorBooks.Add(new AuthorBook { AuthorId = authorId, BookId = id });
                    }

                    foreach (var categoryId in viewModel.SelectedCategoryIds)
                    {
                        _context.CategoryBooks.Add(new CategoryBook { CategoryId = categoryId, BookId = id });
                    }

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(viewModel.Book.Id)) return NotFound();
                    else throw;
                }

                return RedirectToAction(nameof(Index));
            }

            viewModel.Authors = new MultiSelectList(_context.Authors, "Id", "Name", viewModel.SelectedAuthorIds);
            viewModel.Categories = new MultiSelectList(_context.Categories, "Id", "Name", viewModel.SelectedCategoryIds);
            viewModel.Users = new SelectList(_context.Users, "Id", "Name", viewModel.Book.UserId);

            return View(viewModel);
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var book = await _context.Books
                .Include(b => b.Ratings)
                .Include(b => b.User)
                .Include(b => b.AuthorBooks).ThenInclude(ab => ab.Author)
                .Include(b => b.CategoryBooks).ThenInclude(cb => cb.Category)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (book == null) return NotFound();

            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var book = await _context.Books
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (book == null) return NotFound();

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }
    }
}
