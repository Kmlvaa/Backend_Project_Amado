namespace Backend_Project_Amado.Entities
{
    public class ProductImage
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int ImageId { get; set; }
        public Product Product { get; set;}
        public Images Image { get; set;}
        public int SortOrder { get; set; }
    }
}
