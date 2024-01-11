using Backend_Project_Amado.Areas.Admin.Models;
using Backend_Project_Amado.Data;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Project_Amado.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SubscriberController : Controller
    {
        private AppDbContext _dbContext;

        public SubscriberController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            var subscribers = _dbContext.Subscribers.ToList();

            var model = new SubscriberIndexVM { Subscribers = subscribers };

            return View(model);
        }
    }
}
