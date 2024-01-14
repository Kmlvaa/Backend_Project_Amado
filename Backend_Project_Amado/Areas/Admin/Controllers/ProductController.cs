using Backend_Project_Amado.Areas.Admin.Models;
using Backend_Project_Amado.Data;
using Backend_Project_Amado.Entities;
using Backend_Project_Amado.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace Backend_Project_Amado.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly FileService _fileService;

        public ProductController(AppDbContext appDbContext, FileService fileService)
        {
            _appDbContext = appDbContext;
            _fileService = fileService;
        }
        public IActionResult Index()
        {
            var products = _appDbContext.Products.ToList();

            var model = new ProductIndexVM
            {
                Products = products
            };
            return View(model);
        }

        public IActionResult Add()
        {
            var categories = _appDbContext.Categories.AsNoTracking().ToList();
            var brands = _appDbContext.Brands.AsNoTracking().ToList();
            var colors = _appDbContext.Colors.AsNoTracking().ToList();

            var model = new ProductAddVM
            {
                Categories = categories,
                Brands = brands,
                Colors = colors
            };

            return View(model);
        }
        [HttpPost]
        public IActionResult Add(ProductAddVM model)
        {
            if (!ModelState.IsValid)
            {
                foreach (string message in ModelState.Values.SelectMany(v => v.Errors)
                                    .Select(e => e.ErrorMessage))
                {
                    ModelState.AddModelError("", message);
                }
                return View(model);
            }

            var product = new Product();

            product.Name = model.Name;
            product.Price = model.Price;
            product.Description = model.Description;
            product.InStock = model.InStock;

            var foundCategory = _appDbContext.Categories.FirstOrDefault(x => x.Id == model.CategoryId);

            if (foundCategory is null) return View(model);
            product.Category = foundCategory;

            var foundBrand = _appDbContext.Brands.FirstOrDefault(x => x.Id == model.BrandId);

            if (foundBrand is null) return View();
            product.Brand = foundBrand;

            if(model.ColorId != null)
            {
                var foundColor = _appDbContext.Colors.FirstOrDefault(x => x.Id == model.ColorId);
                if (foundColor is null) return View();

                product.Colors = new()
                {
                    new ProductColor
                    {
                        Color = foundColor
                    }
                };
            }

            var URLs = _fileService.AddFile(model.Files, Path.Combine("img", "product-img"));

            product.Images = URLs.Select(url => new ProductImage
            {
                Image = new Images
                {
                    Url = url,
                }
            }).ToList();

            _appDbContext.Add(product);
            _appDbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Edit(int id)
        {
            var product = _appDbContext.Products
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .Include(x => x.Colors)
                .Include(x => x.Images)
                .ThenInclude(x => x.Image)
                .FirstOrDefault(x => x.Id == id);

            if (product is null) return NotFound();

            var categories = _appDbContext.Categories.ToList();
            var brands = _appDbContext.Brands.ToList();
            var colors = _appDbContext.Colors.ToList();

            List<string> currentImageUrls = product.Images?
                .Select(x => x.Image.Url)
                .ToList() ?? new List<string>();

            ProductEditVM updatedModel = new()
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                InStock = product.InStock,
                CategoryId = product.CategoryId,
                BrandId = product.BrandId,
                Categories = categories,
                Brands = brands,
                Colors = colors,
                ColorId = product.Colors.FirstOrDefault().ColorId,
                CurrentImage = currentImageUrls
            };

            return View(updatedModel);
        }
        [HttpPost]
        public IActionResult Edit(ProductEditVM model)
        {
            if (!ModelState.IsValid)
            {
                foreach (string message in ModelState.Values.SelectMany(v => v.Errors)
                                    .Select(e => e.ErrorMessage))
                {
                    ModelState.AddModelError("", message);
                }
                return View(model);
            }

            var product = _appDbContext.Products
                .Include(x => x.Category)
                .Include(x => x.Brand)
                .Include (x => x.Colors)
                .Include(x => x.Images)
                .FirstOrDefault(x => x.Id == model.Id);

            if (product == null) return NotFound();

            product.Name = model.Name;
            product.Price = model.Price;
            product.Description = model.Description;
            product.InStock = model.InStock;
            product.CategoryId = model.CategoryId;
            product.BrandId = model.BrandId;
            
            var category = _appDbContext.Categories.FirstOrDefault(x => x.Id == model.CategoryId);
            if (category == null) return NotFound();
            product.Category = category;

            var brand = _appDbContext.Brands.FirstOrDefault(x => x.Id == model.BrandId);
            if (brand == null) return NotFound();
            product.Brand = brand;

            var color = _appDbContext.Colors.FirstOrDefault(x => x.Id == model.ColorId);
            if (color == null) return NotFound();

            product.Colors = new List<ProductColor>
            {
                new ProductColor
                {
                    ColorId = model.ColorId
                }
            };

            _appDbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int? id)
        {
            var foundedProduct = _appDbContext.Products
                .Include(x => x.Images)
                .ThenInclude(x => x.Image)
                .FirstOrDefault(x => x.Id == id);

            if (foundedProduct is null) return NotFound();

            if (foundedProduct.Images is not null)
            {
                foreach (var image in foundedProduct.Images)
                {
                    if (image.Image != null)
                    {
                        _fileService.DeleteFile(image.Image.Url, Path.Combine("img", "product-img"));
                    }
                }
            }

            _appDbContext.Products.Remove(foundedProduct); 

            _appDbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
