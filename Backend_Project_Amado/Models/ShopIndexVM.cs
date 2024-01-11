using Backend_Project_Amado.Entities;

namespace Backend_Project_Amado.Models
{
    public class ShopIndexVM
    {
        public List<Product> Products { get; set; }
        public List<Images> Images { get; set; }
        public int TotalPageCount { get; set; }
        public int CurrentPage { get; set; }
    }
}
