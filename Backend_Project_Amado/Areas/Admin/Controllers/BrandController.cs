using Microsoft.AspNetCore.Mvc;

namespace Backend_Project_Amado.Areas.Admin.Controllers
{
    public class BrandController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
