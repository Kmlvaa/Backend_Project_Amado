namespace Backend_Project_Amado.Entities
{
    public class Images
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public List<ProductImage> ProductImages { get; set; }
    }
}
