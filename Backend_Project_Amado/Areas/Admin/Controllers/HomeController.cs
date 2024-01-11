using Backend_Project_Amado.Areas.Admin.Models;
using Backend_Project_Amado.Data;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Project_Amado.Areas.Admin.Controllers
{
    [Area("Admin")]
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
            var categories = _dbContext.Categories.ToList();
            var brands = _dbContext.Brands.ToList();

            var model = new HomeIndexVM 
            { 
                Products = products,
                Categories = categories,
                Brands = brands
            };
            return View(model);
        }
    }
}
