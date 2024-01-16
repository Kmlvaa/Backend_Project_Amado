using Backend_Project_Amado.Data;
using Backend_Project_Amado.Entities;
using Backend_Project_Amado.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Project_Amado.Views.Shared.Components.Subscribers
{
    public class SubscriberViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var model = new SubscribersComponentVM();
            return View(model);
        }
    }
}
