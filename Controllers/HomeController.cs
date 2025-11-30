using System.Diagnostics;
using EcomAdmin.Models;
using Microsoft.AspNetCore.Mvc;

namespace EcomAdmin.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Logout()
        {
            var sessionCheck = HttpContext.Session.GetString("username");

            if (!string.IsNullOrEmpty(sessionCheck))
            {
                HttpContext.Session.Clear();
                return RedirectToAction("Index", "Login");
            }
            return View();
            
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}