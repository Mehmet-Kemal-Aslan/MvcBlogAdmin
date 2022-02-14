using Microsoft.AspNetCore.Mvc;
using MvcBlogAdmin.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using MvcBlogAdmin.Filter;


namespace MvcBlogAdmin.Controllers
{
    [UserFilter]

    public class WorkController : Controller
    {
        private readonly ILogger<WorkController> _logger;

        private readonly BlogAdminContext _context;

        public WorkController(ILogger<WorkController> logger, BlogAdminContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> AddCategory(Category category)
        {
            if (category.Id == 0)
            {
                await _context.AddAsync(category);
            }
            else
            {
                _context.Update(category);
            }
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Category));
        }

        public async Task<IActionResult> CategoryDetails(int Id)
        {
            var category = await _context.Category.FindAsync(Id);
            return Json(category);
        }

        public async Task<IActionResult> DeleteCategory(int? Id)
        {
            Category category = await _context.Category.FindAsync(Id);
            _context.Category.Remove(category);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Category));
        }

        public IActionResult Category()
        {
            List<Category> list = _context.Category.ToList();
            return View(list);
        }

        public async Task<IActionResult> AddAuthor(Author author)
        {
            if (author.Id == 0)
            {
                await _context.AddAsync(author);
            }
            else
            {
                _context.Update(author);
            }
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(author));
        }

        public async Task<IActionResult> AuthorDetails(int Id)
        {
            var author = await _context.Author.FindAsync(Id);
            return Json(author);
        }

        public async Task<IActionResult> DeleteAuthor(int? Id)
        {
            Author author = await _context.Author.FindAsync(Id);
            _context.Author.Remove(author);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(author));
        }

        public IActionResult Author()
        {
            List<Author> list = _context.Author.ToList();
            return View(list);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}