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
        public IActionResult Details(int? id)
        {
            if (id is null) return BadRequest();

            Product? product = _appDbContext.Products.Include(x => x.Category)
                .Include(p => p.Brand)
                .Include(p => p.Images).ThenInclude(pi => pi.Image)
                .Include(p => p.Colors).ThenInclude(pi => pi.Color)
                .FirstOrDefault(x => x.Id == id);

            if (product is null) return NotFound();

            ViewBag.Color = product.Colors?.FirstOrDefault()?.Color?.ColorName;

            return View(product);
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
            if (!ModelState.IsValid)  return View(model);

            var product = new Product();

            product.Name = model.Name;
            product.Price = model.Price;
            product.Description = model.Description;

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
        public IActionResult Edit(int id, ProductAddVM model)
        {
            if(!ModelState.IsValid) return View(model);

            var products = _appDbContext.Products
                .Include(x => x.Images)
                .ThenInclude(x => x.Image)
                .FirstOrDefault(x => x.Id == id);

            if (products is null) return View();

            products.Name = model.Name;
            products.Price = model.Price;
            products.Description = model.Description;

            products.Brand.Name = model.Brands.FirstOrDefault().Name;

            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public IActionResult Edit(ProductAddVM model)
        {
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
