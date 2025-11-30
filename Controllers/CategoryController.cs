using EcomAdmin.Data;
using EcomAdmin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcomAdmin.Controllers
{
    public class CategoryController : Controller
    {
        private readonly AppDbContext _dbContext;
        private readonly IWebHostEnvironment _webEnviron;

        public CategoryController(AppDbContext dbContext, IWebHostEnvironment webhost) 
        {
            _dbContext = dbContext;
            _webEnviron = webhost;

        }        

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(Category category)
        {
            if (ModelState.IsValid)
            {
                var dup_category = _dbContext.Categories.AsNoTracking().FirstOrDefault(x => x.Name.ToLower() == category.Name.ToLower());

                if (dup_category == null)
                {
                    var rootpath = _webEnviron.WebRootPath;

                    var originalpath = Path.Combine(rootpath, @"images\");

                    var imgfile = HttpContext.Request.Form.Files;

                    if (imgfile.Count > 0)
                    {

                        var newfilename = Guid.NewGuid().ToString();

                        var fileName = Path.GetFileName(imgfile[0].FileName);

                        using (var filestream = new FileStream(Path.Combine(originalpath, newfilename + "-" + fileName), FileMode.Create))
                        {
                            imgfile[0].CopyTo(filestream);
                        }

                        category.ImageUrl = @"\images\" + newfilename + "-" + fileName;
                    }

                    category.CreatedDate = DateTime.Now;

                    _dbContext.Categories.Add(category);
                    _dbContext.SaveChanges();
                    TempData["Success"] = "New Category Added.";

                    return RedirectToAction("AllCategory");
                }
                else
                {
                    TempData["Warning"] = "This category type already exists.";
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult AllCategory()
        {
            List<Category> all_cat = _dbContext.Categories.ToList();
            return View(all_cat);
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {   
            Category cat1 = _dbContext.Categories.FirstOrDefault(x => x.CategoryId == Id);

            if (cat1 == null)
            {
                return NotFound(); // Or RedirectToAction with a TempData error message
            }

            return View(cat1);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            var dupCat = _dbContext.Categories.AsNoTracking().FirstOrDefault(x => x.Name.ToLower() == category.Name.ToLower() && x.CategoryId != category.CategoryId);
            
            if (dupCat == null)
            {
                var rootPath = _webEnviron.WebRootPath;
                var imageFolder = Path.Combine(rootPath, "images");
                var uploadedFiles = HttpContext.Request.Form.Files;

                if (uploadedFiles.Count > 0)
                {
                    var existingCategory = _dbContext.Categories.AsNoTracking()
                        .FirstOrDefault(x => x.CategoryId == category.CategoryId);

                    if (existingCategory.ImageUrl != null)
                    {
                        var oldImagePath = Path.Combine(rootPath, existingCategory.ImageUrl.Trim('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    var uniqueFileName = Guid.NewGuid().ToString();
                    var newfilename = Path.GetFileName(uploadedFiles[0].FileName);
                    var newImagePath = Path.Combine(imageFolder, uniqueFileName + "-" + newfilename);

                    using (var stream = new FileStream(newImagePath, FileMode.Create))
                    {
                        uploadedFiles[0].CopyTo(stream);
                    }

                    category.ImageUrl = @"\images\" + uniqueFileName + "-" + newfilename;
                }

                if (category.ImageUrl == null)
                {
                    var oldCategory = _dbContext.Categories.AsNoTracking()
                    .FirstOrDefault(x => x.CategoryId == category.CategoryId);
                    category.ImageUrl = oldCategory.ImageUrl;
                }

                category.CreatedDate = DateTime.Now;

                _dbContext.Categories.Update(category);
                _dbContext.SaveChanges();
                TempData["Warning"] = "Category updated successfully.";

                return RedirectToAction("AllCategory");
            }

            TempData["Warning"] = "This Category type already exists.";

            var old_cat = _dbContext.Categories.FirstOrDefault(x => x.CategoryId == category.CategoryId);

            return View(old_cat);            
            
        }

        [HttpGet]
        public IActionResult Delete(int Id)
        {
            Category d_cate = _dbContext.Categories.FirstOrDefault(x => x.CategoryId == Id);

            if (d_cate == null)
            {
                return NotFound(); // Or RedirectToAction with a TempData error message
            }

            return View(d_cate);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Category category)
        {
            Category d_cate1 = _dbContext.Categories.FirstOrDefault(x => x.CategoryId == category.CategoryId);

            if(d_cate1.ImageUrl != null)
            {
                var rootpath = _webEnviron.WebRootPath;                

                var filename = Path.Combine(rootpath, d_cate1.ImageUrl.Trim('\\'));

                if (System.IO.File.Exists(filename))
                {
                    System.IO.File.Delete(filename);
                }

            }

            _dbContext.Categories.Remove(d_cate1);
            _dbContext.SaveChanges();
            TempData["Error"] = "Category deleted successfully.";

            return RedirectToAction("AllCategory");
        }
       
    }
}