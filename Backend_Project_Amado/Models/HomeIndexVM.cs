using Backend_Project_Amado.Entities;

namespace Backend_Project_Amado.Models
{
    public class HomeIndexVM
    {
        public List<Product> Products { get; set; }
        public List<Images> Images { get; set; }
        public List<ProductImage> ProductImages { get; set; }
    }
}
