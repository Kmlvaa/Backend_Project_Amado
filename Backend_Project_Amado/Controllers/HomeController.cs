using Backend_Project_Amado.Data;
using Backend_Project_Amado.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Backend_Project_Amado.Controllers
{
    public class HomeController : Controller
    {
        private AppDbContext _dbContext;

        public HomeController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            var products = _dbContext.Products.ToList();
            var images = _dbContext.Images.ToList();
            var productImages = _dbContext.ProductsImage.ToList();

            var model = new HomeIndexVM
            { 
                Products = products,
                Images = images,
                ProductImages = productImages,
            };
            return View(model);
        }

    }
}