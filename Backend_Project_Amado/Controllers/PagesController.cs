using Backend_Project_Amado.Data;
using Backend_Project_Amado.Entities;
using Backend_Project_Amado.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.ContentModel;
using System.Text.Json;

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
            Request.Cookies.TryGetValue("basket", out var basketSerialized);

            Basket basket = null!;
            if (basketSerialized is null)
            {
                basket = new Basket();
            }
            else
            {
                basket = JsonSerializer.Deserialize<Basket>(basketSerialized)!;
            }

            List<(BasketItem, Product)> items = new();

            foreach (var basketItem in basket.BasketItems)
            {
                Product product = _dbContext.Products?.Include(p => p.Images).ThenInclude(p => p.Image).FirstOrDefault(x => x.Id == basketItem.ProductId)!;

                items.Add(new(basketItem, product));
            }
            var total = items.Sum(item => item.Item2.Price * item.Item1.Count);
            var model = new PagesCartVM
            {
                Items = items,
                Count = items.Count(),
                Total = total
            };

            return View(model);

        }
        public IActionResult AddToBasket(int? id)
        {
            if (id is null) return NotFound();

            var foundProduct = _dbContext.Products.FirstOrDefault(x => x.Id == id);

            if (foundProduct is null) return NotFound();

            Request.Cookies.TryGetValue("basket", out var basketSerialized);

            Basket basket = null!;
            if (basketSerialized is null)
            {
                basket = new Basket();
            }
            else
            {
                basket = JsonSerializer.Deserialize<Basket>(basketSerialized)!;
            }
            var foundBasketItem = basket.BasketItems.FirstOrDefault(x => x.ProductId == foundProduct.Id);

            if (foundBasketItem is null)
            {
                foundBasketItem = new BasketItem
                {
                    ProductId = foundProduct.Id,
                    Count = 1
                };

                basket.BasketItems.Add(foundBasketItem);
            }
            else
            {
                foundBasketItem.Count++;
            }

            Response.Cookies.Append("basket", JsonSerializer.Serialize(basket));

            return RedirectToAction("Index", "Shop");
        }
        public IActionResult DeleteFromBasket(int? id)
        {
            if (id is null) return NotFound();

            Request.Cookies.TryGetValue("basket", out var basketSerialized);

            if (basketSerialized is null) return RedirectToAction("Index", "Home");

            var basket = JsonSerializer.Deserialize<Basket>(basketSerialized)!;

            var foundBasketItem = basket.BasketItems.FirstOrDefault(x => x.ProductId == id);

            if (foundBasketItem is null) return NotFound();

            basket.BasketItems.Remove(foundBasketItem);

            Response.Cookies.Append("basket", JsonSerializer.Serialize(basket));

            return RedirectToAction("Cart", "Pages");
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
