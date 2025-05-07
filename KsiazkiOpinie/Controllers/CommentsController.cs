using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KsiazkiOpinie.Data;
using KsiazkiOpinie.Models;

namespace KsiazkiOpinie.Controllers
{
    public class CommentsController : Controller
    {
        private readonly BookAppContext _context;

        public CommentsController(BookAppContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> ViewComment(int id)
        {
            var book = await _context.Books
                .Include(b => b.Comments)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddComment([Bind("Content,AuthorName,BookId")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                _context.Comments.Add(comment);
                await _context.SaveChangesAsync();
                return RedirectToAction("ViewComment", new { id = comment.BookId });
            }

            var book = await _context.Books
                .Include(b => b.Comments)
                .FirstOrDefaultAsync(b => b.Id == comment.BookId);

            return View("ViewComment", book);
        }

        public async Task<IActionResult> Index()
        {
            var bookAppContext = _context.Comments.Include(c => c.Book);
            return View(await bookAppContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var comment = await _context.Comments
                .Include(c => c.Book)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (comment == null) return NotFound();

            return View(comment);
        }

        public IActionResult Create()
        {
            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Id");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Content,AuthorName,BookId")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(comment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Id", comment.BookId);
            return View(comment);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var comment = await _context.Comments.FindAsync(id);
            if (comment == null) return NotFound();

            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Id", comment.BookId);
            return View(comment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Content,AuthorName,BookId")] Comment comment)
        {
            if (id != comment.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommentExists(comment.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["BookId"] = new SelectList(_context.Books, "Id", "Id", comment.BookId);
            return View(comment);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var comment = await _context.Comments
                .Include(c => c.Book)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (comment == null) return NotFound();

            return View(comment);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment != null)
            {
                _context.Comments.Remove(comment);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool CommentExists(int id)
        {
            return _context.Comments.Any(e => e.Id == id);
        }
    }
}
