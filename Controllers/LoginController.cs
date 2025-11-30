using EcomAdmin.Data;
using EcomAdmin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcomAdmin.Controllers
{
    public class LoginController : Controller
    {
        private readonly AppDbContext _dbContext;

        public LoginController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            HttpContext.Session.Clear();
            return View();
        }

        [HttpPost]
        public IActionResult Index(Login login)
        {
            if (!string.IsNullOrEmpty(login.uname) && !string.IsNullOrEmpty(login.pass))
    {
                var user = _dbContext.adminlogin
                    .FirstOrDefault(x => x.uname == login.uname && x.pass == login.pass);

                if (user != null)
                {
                    HttpContext.Session.SetString("username", user.uname);
                    HttpContext.Session.SetString("allowadmin", user.access.ToString());
                    TempData["Success"] = "Login successfully.";
                    return RedirectToAction("Index", "Home");                    
                }
                else if (login.uname == "admin" && login.pass == "admin")
                {
                    login.createddate = DateTime.Now;
                    login.access = true;
                    _dbContext.adminlogin.Add(login);
                    _dbContext.SaveChanges();
                    HttpContext.Session.SetString("username", login.uname);
                    HttpContext.Session.SetString("allowadmin", login.access.ToString());
                    TempData["Success"] = "Login successfully.";
                    return RedirectToAction("Index", "Home");                    
                }

                TempData["error"] = "Invalid username or password.";
            }           
            return View();
        }
    }
}
