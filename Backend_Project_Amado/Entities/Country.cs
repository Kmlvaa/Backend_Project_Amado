namespace Backend_Project_Amado.Entities
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Checkout> checkouts { get; set; }
    }
}
