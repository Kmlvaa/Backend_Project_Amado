using Microsoft.AspNetCore.Mvc;

namespace Backend_Project_Amado.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
