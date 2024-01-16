using Backend_Project_Amado.Areas.Admin.Models;
using Backend_Project_Amado.Data;
using Backend_Project_Amado.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Differencing;
using Microsoft.EntityFrameworkCore;

namespace Backend_Project_Amado.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        public AppDbContext _dbContext;

        public CategoryController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            var categories = _dbContext.Categories.ToList();

            var model = new HomeIndexVM
            {
                Categories = categories
            };
            return View(model);
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Add(CategoryAddVM model)
        {
            if (!ModelState.IsValid) return NotFound();

            var category = new Category();

            category.Name = model.Category;

            _dbContext.Add(category);
            _dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        public ActionResult Edit(int id)
        {
            var category = _dbContext.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null) return View();

            var model = new CategoryAddVM
            {
                Category = category.Name
            };
            return View(model);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id, CategoryAddVM editedCategory)
        {
            if (!ModelState.IsValid) return NotFound();

            if (id != editedCategory.Id) return BadRequest();

            var category = _dbContext.Categories.FirstOrDefault(x => x.Id == id);

            if (category is null) return NotFound();

            bool duplicate = _dbContext.Categories.Any(c => c.Name == editedCategory.Category && editedCategory.Category != category.Name);
            if (duplicate)
            {
                ModelState.AddModelError("", "You cannot duplicate category name");
                return View(category);
            }
            category.Name = editedCategory.Category;
            _dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var foundCategory = _dbContext.Categories.FirstOrDefault(c => c.Id == id);
            if (foundCategory is null)  return NotFound(); 
            
            _dbContext.Categories.Remove(foundCategory);
            _dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
