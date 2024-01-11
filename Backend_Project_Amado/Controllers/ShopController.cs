using Backend_Project_Amado.Data;
using Backend_Project_Amado.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Project_Amado.Controllers
{
    public class ShopController : Controller
    {
        private AppDbContext _dbContext;

        public ShopController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index(int page, int productsPerPage = 3, string order = "desc")
        {
            if (page <= 1) page = 1;
            int ProductsPerPage = productsPerPage;

            var productCount = _dbContext.Products.Count();
            var totalPageCount = (int)Math.Ceiling((decimal)productCount / ProductsPerPage);

            var products = order switch
            {
                "desc" => _dbContext.Products.OrderByDescending(x => x.Id),
                "asc" => _dbContext.Products.OrderBy(x => x.Id),
                _ => _dbContext.Products.OrderByDescending(x => x.Id)
            };
            var model = new ShopIndexVM
            {
                Products = products
                .Skip((page - 1) * ProductsPerPage)
                .Take(productsPerPage)
                .ToList(),
                CurrentPage = page,
                TotalPageCount = totalPageCount
            };

            return View(model);
        }
        public IActionResult ProductDetails()
        {
            return View();
        }
    }
}
