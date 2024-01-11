using Backend_Project_Amado.Areas.Admin.Models;
using Backend_Project_Amado.Data;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Project_Amado.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CheckoutController : Controller
    {
        private AppDbContext _dbContext;

        public CheckoutController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            var checkouts = _dbContext.Checkouts.ToList();

            var model = new CheckoutIndexVM
            {
                Checkouts = checkouts
            };
            return View(model);
        }
    }
}
