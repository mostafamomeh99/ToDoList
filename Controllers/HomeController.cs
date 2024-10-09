using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ToDoList.Data;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        ApplicationDbContext context = new ApplicationDbContext();
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
      
        public IActionResult CreateUser()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateUser(string Name)
        {   
            var user=context.Users.Where(e=>e.Name.ToLower()==Name.ToLower()).FirstOrDefault();
            if(user == null)
            {
                context.Users.Add(new User() { Name= Name });
                context.SaveChanges();
                CookieOptions cookieOptions = new CookieOptions();
                cookieOptions.Expires=DateTime.Now.AddDays(1);
                Response.Cookies.Append("username", Name, cookieOptions);
            }
            else
            { 
                CookieOptions cookieOptions = new CookieOptions();
                cookieOptions.Expires = DateTime.Now.AddDays(1);
                Response.Cookies.Append("username", Name, cookieOptions);
            }
            return RedirectToAction("Index","ToDo");
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
