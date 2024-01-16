using Backend_Project_Amado.Areas.Admin.Models;
using Backend_Project_Amado.Data;
using Backend_Project_Amado.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Project_Amado.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BrandController : Controller
    {
        private AppDbContext _dbContext;
        public BrandController(AppDbContext appDbContext)
        {
            _dbContext = appDbContext;
        }
        public IActionResult Index()
        {
            var brands = _dbContext.Brands.ToList();

            var model = new HomeIndexVM
            {
                Brands = brands
            };
            return View(model);
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Add(BrandAddVM model)
        {
            if (!ModelState.IsValid) return NotFound();
            var brand = new Brand();

            brand.Name = model.Brand;

            _dbContext.Add(brand);
            _dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Edit(int id)
        {
            var brand = _dbContext.Brands.FirstOrDefault(b => b.Id == id);
            if(brand is null) return View();

            var model = new BrandAddVM
            {
                Brand = brand.Name
            };
            return View(model);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id, BrandAddVM model)
        {
            if (!ModelState.IsValid) return NotFound();

            if (id != model.Id) return BadRequest();

            var brand = _dbContext.Brands.FirstOrDefault(x => x.Id == id);

            if (brand is null) return NotFound();

            bool duplicate = _dbContext.Brands.Any(c => c.Name == model.Brand && model.Brand != brand.Name);
            if (duplicate)
            {
                ModelState.AddModelError("", "You cannot duplicate category name");
                return View(brand);
            }
            brand.Name = model.Brand;
            _dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var foundBrand = _dbContext.Brands.FirstOrDefault(x => x.Id == id);
            if (foundBrand is null) return NotFound();

            _dbContext.Brands.Remove(foundBrand);
            _dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
