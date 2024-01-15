﻿using Backend_Project_Amado.Data;
using Backend_Project_Amado.Entities;
using Backend_Project_Amado.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend_Project_Amado.Controllers
{
    public class ShopController : Controller
    {
        private AppDbContext _dbContext;

        public ShopController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index(int page, string category = "Chairs", int view = 4, string order = "desc")
        {
            if (page <= 1) page = 1;
            int ProductsPerPage = view;

            int startPoint = 1;
            int endPoint = view;

            if(page > 1)
            {
                startPoint = (page - 1) * view;
                endPoint = page * view;
            }

            string categoryName = category;

            var filtered = _dbContext.Products.FirstOrDefault(x => x.Category.Name == categoryName);

            var productCount = _dbContext.Products.Count();
            var totalPageCount = (int)Math.Ceiling((decimal)productCount / ProductsPerPage);

            var products = order switch
            {
                "desc" => _dbContext.Products.OrderByDescending(x => x.Id),
                "asc" => _dbContext.Products.OrderBy(x => x.Id),
                _ => _dbContext.Products.OrderByDescending(x => x.Id)
            };

            ViewData["OrderById"] = string.IsNullOrEmpty(order) ? "desc" : "asc";

            var categories = _dbContext.Categories.AsNoTracking().ToList();
            var brands = _dbContext.Brands.AsNoTracking().ToList();
            var colors = _dbContext.Colors.AsNoTracking().ToList();

            var model = new ShopIndexVM
            {
                Products = products
                .Skip((page - 1) * ProductsPerPage)
                .Take(ProductsPerPage)
                .ToList(),
                CurrentPage = page,
                TotalPageCount = totalPageCount,
                Categories = categories,
                Brands = brands,
                Colors = colors,
                ProductCountPerPage = view,
                StartPoint = startPoint,
                EndPoint = endPoint,
                ProductCount = productCount
            };

            ViewBag.Order = order;

            return View(model);
        }
        public IActionResult ProductDetails(int? id)
        {
            if (id is null) return NotFound();

            Product? product = _dbContext.Products
                .Include(x => x.Images).ThenInclude(x => x.Image)
                .FirstOrDefault(x => x.Id == id);

            if (product is null) return NotFound();

            var category = _dbContext.Categories.FirstOrDefault(x => x.Id == product.CategoryId);
            var brand = _dbContext.Brands.FirstOrDefault(x => x.Id == product.BrandId);

            ShopIndexVM model = new()
            {
                Product = product,
                CategoryName = category.Name,
                BrandName = category.Name,
            };

            return View(model);
        }
        public IActionResult Search(string? input)
        {
            var products = input == null ? new List<Product>()
                : _dbContext.Products
                .Where(x => x.Name.ToLower()
                .StartsWith(input.ToLower()))
                .ToList();

            return ViewComponent("SearchResult", products);
        }
    }
}
