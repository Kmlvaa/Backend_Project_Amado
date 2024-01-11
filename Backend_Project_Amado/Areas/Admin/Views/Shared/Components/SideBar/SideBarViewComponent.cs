using Microsoft.AspNetCore.Mvc;

namespace Backend_Project_Amado.Areas.Admin.Views.Shared.Components.SideBar
{
    public class SideBarViewComponent :ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
