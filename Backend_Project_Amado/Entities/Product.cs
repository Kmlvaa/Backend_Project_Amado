using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend_Project_Amado.Entities
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public string Description { get; set; }

        [Column(TypeName = "money")]
        public decimal Price { get; set; }
        public bool InStock { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set;}
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        public List<ProductImage> Images { get; set; }
        public List<ProductColor> Colors { get; set; }

    }
}
