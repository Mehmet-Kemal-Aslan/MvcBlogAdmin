using Microsoft.AspNetCore.Mvc;
using MvcBlogAdmin.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using MvcBlogAdmin.Filter;


namespace MvcBlogAdmin.Controllers
{
    //[UserFilter]

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly BlogAdminContext _context;

        public HomeController(ILogger<HomeController> logger, BlogAdminContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Login(string Email, string Password)
        {
            var author = _context.Author.FirstOrDefault(w => w.Email == Email && w.Password == Password);
            if (author == null)
            {
                return RedirectToAction(nameof(Index));
            }
            HttpContext.Session.SetInt32("id", author.Id);
            return RedirectToAction(nameof(Category));
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Category()
        {
            if (!HttpContext.Session.GetInt32("id").HasValue)
            {
                return Redirect("/Home/Index");
            }
            List<Category> list= _context.Category.ToList();
            return View(list);
        }


        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("id").HasValue)
            {
                return Redirect("/Home/Category");
            }
            return View();
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