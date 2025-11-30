using EcomAdmin.Data;
using EcomAdmin.Models;
using EcomAdmin.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EcomAdmin.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _dbContext;
        private readonly IWebHostEnvironment _webEnviron;

        public ProductController(AppDbContext context, IWebHostEnvironment environment)
        {
            _dbContext = context;
            _webEnviron = environment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<Product> prod = _dbContext.Products.ToList();
            return View(prod);
        }


        [HttpGet]
        public IActionResult Create()
        {
            ProductViewModel viewModel = new ProductViewModel
            {
                Product = new Product(),
                CategoryList = _dbContext.Categories
            .Select(c => new SelectListItem
            {
                Value = c.CategoryId.ToString(),
                Text = c.Name
            }).ToList()
            };

            return View(viewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                var dup_product = _dbContext.Products.AsNoTracking().FirstOrDefault(x => x.Name.ToLower() == product.Name.ToLower());

                if (dup_product == null)
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

                        product.ImageUrl = @"\images\" + newfilename + "-" + fileName;
                    }

                    product.CreatedDate = DateTime.Now;

                    _dbContext.Products.Add(product);
                    _dbContext.SaveChanges();
                    TempData["Success"] = "New Product Added.";

                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Warning"] = "This Product already exists.";
                }
            }

            ProductViewModel viewModel = new ProductViewModel
            {
                Product = new Product(),
                CategoryList = _dbContext.Categories
            .Select(c => new SelectListItem
            {
                Value = c.CategoryId.ToString(),
                Text = c.Name
            }).ToList()
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Product prod = _dbContext.Products.FirstOrDefault(x => x.ProductId == id);

            if (prod == null)
            {
                return NotFound();
            }

            ProductViewModel viewModel = new ProductViewModel
            {
                Product = prod,
                CategoryList = _dbContext.Categories
            .Select(c => new SelectListItem
            {
                Value = c.CategoryId.ToString(),
                Text = c.Name
            }).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                var dupProd = _dbContext.Products.AsNoTracking().FirstOrDefault(x => x.Name.ToLower() == product.Name.ToLower() && x.ProductId != product.ProductId);
                
                if (dupProd == null)
                {
                    var rootPath = _webEnviron.WebRootPath;
                    var imageFolder = Path.Combine(rootPath, "images");
                    var uploadedFiles = HttpContext.Request.Form.Files;

                    if (uploadedFiles.Count > 0)
                    {
                        var existingProduct = _dbContext.Products.AsNoTracking()
                            .FirstOrDefault(x => x.CategoryId == product.CategoryId);

                        if (existingProduct.ImageUrl != null)
                        {
                            var oldImagePath = Path.Combine(rootPath, existingProduct.ImageUrl.Trim('\\'));
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

                        product.ImageUrl = @"\images\" + uniqueFileName + "-" + newfilename;
                    }

                    if (product.ImageUrl == null)
                    {
                        var oldCategory = _dbContext.Products.AsNoTracking()
                        .FirstOrDefault(x => x.CategoryId == product.CategoryId);
                        product.ImageUrl = oldCategory.ImageUrl;
                    }

                    product.CreatedDate = DateTime.Now;

                    _dbContext.Products.Update(product);
                    _dbContext.SaveChanges();
                    TempData["Warning"] = "Product updated successfully.";

                    return RedirectToAction("Index");
                }

                TempData["Warning"] = "This Product already exists.";

            }

            var old_prod = _dbContext.Products.FirstOrDefault(x => x.ProductId == product.ProductId);            

            ProductViewModel viewModel = new ProductViewModel
            {
                Product = old_prod,
                CategoryList = _dbContext.Categories
            .Select(c => new SelectListItem
            {
                Value = c.CategoryId.ToString(),
                Text = c.Name
            }).ToList()
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Product dProd = _dbContext.Products.FirstOrDefault(x => x.ProductId == id);
            if (dProd == null)
            {
                return NotFound();
            }
            return View(dProd);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Product product)
        {
            Product dProd1 = _dbContext.Products.FirstOrDefault(x => x.ProductId == product.ProductId);

            if (dProd1.ImageUrl != null)
            {
                var rootpath = _webEnviron.WebRootPath;

                var filename = Path.Combine(rootpath, dProd1.ImageUrl.Trim('\\'));

                if (System.IO.File.Exists(filename))
                {
                    System.IO.File.Delete(filename);
                }

            }

            _dbContext.Products.Remove(dProd1);
            _dbContext.SaveChanges();
            TempData["Error"] = "Product deleted successfully.";

            return RedirectToAction("Index");
            
        }
    }
}
