﻿using Backend_Project_Amado.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend_Project_Amado.Areas.Admin.Models
{
    public class ProductEditVM
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        public string Description { get; set; }
        public bool InStock { get; set; }
        public int CategoryId { get; set; }
        [ValidateNever]
        public List<Category> Categories { get; set; }
        public int BrandId { get; set; }
        [ValidateNever]
        public List<Brand> Brands { get; set; }
        public int ColorId { get; set; }

        [ValidateNever]
        public List<Color> Colors { get; set; }
        [ValidateNever]
        public List<IFormFile> Files { get; set; }
        [ValidateNever]
        public List<string> CurrentImage { get; set; }
        public List<int> DeletedImageIds { get; set; }
        [NotMapped]
        public List<int>? ImagesId { get; set; }
        [NotMapped]
        public List<Images>? AllImages { get; set; }
    }
}
