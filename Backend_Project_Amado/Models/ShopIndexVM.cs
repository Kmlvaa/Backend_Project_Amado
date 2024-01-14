using Backend_Project_Amado.Entities;

namespace Backend_Project_Amado.Models
{
    public class ShopIndexVM
    {
        public List<Product> Products { get; set; }
        public List<Images> Images { get; set; }
        public int TotalPageCount { get; set; }
        public int CurrentPage { get; set; }
        public int ProductCountPerPage { get; set; }
        public List<Category> Categories { get; set; }
        public List<Brand> Brands { get; set; }
        public List<Color> Colors { get; set; }
        public int PageCount()
        {
            return Products.Count * TotalPageCount;
        }
    }
}
