using Microsoft.AspNetCore.Mvc;
using MvcBlogAdmin.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcBlogAdmin.Filter;


namespace MvcBlogAdmin.Controllers
{
    [UserFilter]

    public class BlogController : Controller
    {
        private readonly ILogger<BlogController> _logger;

        private readonly BlogAdminContext _context;

        public BlogController(ILogger<BlogController> logger, BlogAdminContext context)
        {
            _logger = logger;
            _context = context;
        }       

        public IActionResult Index()
        {
            var list = _context.Blog.ToList();
            return View(list);
        }
        public IActionResult Blog()
        {
            ViewBag.Categories = _context.Category.Select(x =>
            new SelectListItem { Text = x.Name, Value = x.Id.ToString() }
            ).ToList();
            return View();
        }

        public IActionResult Publish(int Id)
        {
            var Blog = _context.Blog.Find(Id);
            Blog.IsPublish = true;
            _context.Update(Blog);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Save(Blog model)
        {
            if (model != null)
            {
                var file = Request.Form.Files.First();
                string savePath = Path.Combine("C:", "Users", "Mehmet Kemal", "source", "repos", "Mvcblog", "Mvcblog", "wwwroot", "assets", "img");
                var fileName = $"{DateTime.Now:MMddHHmmss}.{file.FileName.Split(".").Last()}";
                var fileUrl = Path.Combine(savePath, fileName);
                using(var fileStream = new FileStream(fileUrl, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                model.ImagePath = fileName;
                model.AuthorId = (int)HttpContext.Session.GetInt32("id");
                await _context.AddAsync(model);
                await _context.SaveChangesAsync();
                return Json(true);
            }

            return Json(false);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}