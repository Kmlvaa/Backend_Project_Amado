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

        public IActionResult AddProduct()
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
        public IActionResult AddProduct(ProductAddVM model)
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

            return View();
        }
        public IActionResult UpdateProduct()
        {
            return View();
        }
        [HttpPost]
        public IActionResult UpdateProduct(ProductAddVM model)
        {
            return View();
        }
        public IActionResult Delete(int? id)
        {
            var product = _appDbContext.Products.FirstOrDefault(x => x.Id == id);
            if (product is null) return View();

            var foundedproduct = product.Images.FirstOrDefault(x => x.ProductId == id);
            if (foundedproduct is null) return NotFound();

            var image = _appDbContext.Images.FirstOrDefault(x => x.Id == foundedproduct.ImageId);

            _fileService.DeleteFile(image.Url, Path.Combine("img", "product"));

            _appDbContext.Remove(product);      
            _appDbContext.Remove(image);

            _appDbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
