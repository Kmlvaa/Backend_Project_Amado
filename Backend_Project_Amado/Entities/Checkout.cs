using System.ComponentModel.DataAnnotations;

namespace Backend_Project_Amado.Entities
{
    public class Checkout
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        [Required]
        public string EmailAddress { get; set; }

        [StringLength(255)]
        public string Address { get; set; }
        public string Town { get; set; }
        public string PhoneNumber { get; set; }
        public int ZipCode { get; set; }
        [StringLength(255)]
        public string Comment { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }

    }
}
