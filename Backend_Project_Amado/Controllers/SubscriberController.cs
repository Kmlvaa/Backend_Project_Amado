﻿using Backend_Project_Amado.Data;
using Backend_Project_Amado.Entities;
using Backend_Project_Amado.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Backend_Project_Amado.Controllers
{
    public class SubscriberController : Controller
    {
        private AppDbContext _dbContext;

        public SubscriberController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(SubscribersComponentVM model)
        {
            if (!ModelState.IsValid) return View(model);

            bool isDuplicated = _dbContext.Subscribers
                                .Any(c => c.EmailAddress == model.EmailAddress);

            if (isDuplicated)
            {
                return RedirectToAction("Index", "Home");
            }

            var subscriber = new Subscriber
            {
                EmailAddress = model.EmailAddress
            };

            _dbContext.Add(subscriber);
            _dbContext.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}
