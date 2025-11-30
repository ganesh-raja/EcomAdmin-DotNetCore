using EcomAdmin.Data;
using EcomAdmin.Models;
using EcomAdmin.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EcomAdmin.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDbContext _dbContext;

        public AdminController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            AdminViewModel admin = new AdminViewModel
            {
                NewAdmin = new Login(),
                AdminList = _dbContext.adminlogin.ToList()
            };
            return View(admin);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(AdminViewModel log)
        {
            if (ModelState.IsValid)
            {
                var dup_user = _dbContext.adminlogin.FirstOrDefault(x => x.uname == log.NewAdmin.uname);
                if (dup_user == null)
                {
                    log.NewAdmin.createddate = DateTime.Now;                    
                    _dbContext.adminlogin.Add(log.NewAdmin);
                    _dbContext.SaveChanges();
                    TempData["Success"] = "New Admin user added.";
                }
                else
                {
                    TempData["Warning"] = "This Admin user already available.";
                }
                
            }
            return RedirectToAction("Index");
        }
        
        public IActionResult Delete(int id)
        {
            var duser = _dbContext.adminlogin.FirstOrDefault(x => x.id == id);

            if (duser != null)
            {
                _dbContext.adminlogin.Remove(duser);
                _dbContext.SaveChanges();
                TempData["Error"] = "Admin user deleted.";
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var Euser = _dbContext.adminlogin.FirstOrDefault(x => x.id == id);

            if (Euser == null)
            {
                return NotFound();
            }
            return View(Euser);
        }

        [HttpPost]
        public IActionResult Edit(Login login)
        {
            if (ModelState.IsValid)
            {
                var Euser = _dbContext.adminlogin
                    .AsNoTracking()
                    .FirstOrDefault(x => x.id != login.id && x.uname == login.uname);

                if (Euser == null)
                {
                    login.createddate = DateTime.Now;
                    _dbContext.adminlogin.Update(login);
                    _dbContext.SaveChanges();
                    TempData["Warning"] = "Admin user updated successfully.";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Warning"] = "This Admin username already exists.";
                }
            }

            return View(login);
        }
    }
}
