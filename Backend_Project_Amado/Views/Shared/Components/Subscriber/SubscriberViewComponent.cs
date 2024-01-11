using Backend_Project_Amado.Data;
using Backend_Project_Amado.Entities;
using Backend_Project_Amado.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Project_Amado.Views.Shared.Components.Subscribers
{
    public class SubscriberViewComponent : ViewComponent
    {
        private AppDbContext _dbContext;

        public SubscriberViewComponent(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IViewComponentResult Invoke(SubscribersComponentVM model)
        {
            if (model is null) return View(model);

            var subscriber = new Subscriber();
            subscriber.EmailAddress = model.EmailAddress;

            _dbContext.Add(subscriber);
            _dbContext.SaveChanges();

            return View(model);
        }
    }
}
