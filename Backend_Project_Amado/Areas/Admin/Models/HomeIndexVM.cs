using Backend_Project_Amado.Entities;

namespace Backend_Project_Amado.Areas.Admin.Models
{
    public class HomeIndexVM
    {
        public List<Product> Products { get; set; }
        public List<Category> Categories { get; set;}
        public List<Brand> Brands { get; set; }
    }
}
