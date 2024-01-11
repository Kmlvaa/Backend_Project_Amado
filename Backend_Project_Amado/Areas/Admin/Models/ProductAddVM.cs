﻿using Backend_Project_Amado.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Backend_Project_Amado.Areas.Admin.Models
{
    public class ProductAddVM
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [DataType(DataType.Currency)]
        public decimal? Price { get; set; }
        public string Description { get; set; }
        public bool IsInStock { get; set; }
        public int CategoryId { get; set; }
        [ValidateNever]
        public List<Category> Categories { get; set; }
        public int BrandId { get; set; }
        [ValidateNever]
        public List<Brand> Brands { get; set;}
        [ValidateNever]
        public int? ColorId { get; set; }

        [ValidateNever]
        public List<Color> Colors { get; set; }

        public IFormFile Image { get; set; }
    }
}