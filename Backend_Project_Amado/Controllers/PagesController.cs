using Backend_Project_Amado.Data;
using Backend_Project_Amado.Entities;
using Backend_Project_Amado.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.ContentModel;

namespace Backend_Project_Amado.Controllers
{
    public class PagesController : Controller
    {
        private AppDbContext _dbContext;

        public PagesController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Cart()
        {
            return View();
        }
        public IActionResult Checkout()
        {
            var countries = _dbContext.Countries.AsNoTracking().ToList();

            var model = new PagesCheckoutVM
            {
                Countries = countries
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult Checkout(PagesCheckoutVM model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            var checkout = new Checkout();

            checkout.PhoneNumber = model.PhoneNumber;
            checkout.FirstName = model.FirstName;
            checkout.LastName = model.LastName;
            checkout.Address = model.Address;
            checkout.EmailAddress = model.EmailAddress;
            checkout.CompanyName = model.CompanyName;
            checkout.Comment = model.Comment;
            checkout.ZipCode = model.ZipCode;
            checkout.Town = model.Town;
            
            var country = _dbContext.Countries.FirstOrDefault(x => x.Id == model.CountryId);
            if (country is null) return View(model);
            checkout.CountryId = country.Id;

            _dbContext.Add(checkout);
            _dbContext.SaveChanges();

            return RedirectToAction(nameof(Checkout));
        }
    }
}
