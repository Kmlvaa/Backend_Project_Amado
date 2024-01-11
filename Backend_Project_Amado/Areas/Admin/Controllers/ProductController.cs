using Backend_Project_Amado.Areas.Admin.Models;
using Backend_Project_Amado.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend_Project_Amado.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        private AppDbContext _appDbContext;

        public ProductController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IActionResult AddProduct()
        {
            var products = _appDbContext.Products.AsNoTracking().ToList();

            return View();
        }
        [HttpPost]
        public IActionResult AddProduct(ProductAddVM model)
        {
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
            return View();
        }
    }
}
