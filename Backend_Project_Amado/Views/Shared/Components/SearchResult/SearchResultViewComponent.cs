using Backend_Project_Amado.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Project_Amado.Views.Shared.Components.SearchResult
{
    public class SearchResultViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(List<Product> model)
        {
            return View(model);
        }
    }
}
