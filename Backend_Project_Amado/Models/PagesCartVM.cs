using Backend_Project_Amado.Entities;

namespace Backend_Project_Amado.Models
{
    public class PagesCartVM
    {
        public List<(BasketItem, Product)> Items { get; set; }
        public int Count { get; set; }
        public decimal Total { get; set; }
    }
}
