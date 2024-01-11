using Backend_Project_Amado.Entities;
using System.ComponentModel.DataAnnotations;

namespace Backend_Project_Amado.Models
{
    public class PagesCheckoutVM
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public string EmailAddress { get; set; }

        public string Address { get; set; }
        public string Town { get; set; }
        public string PhoneNumber { get; set; }
        public int ZipCode { get; set; }
        public string Comment { get; set; }
        public int CountryId { get; set; }
        public List<Country> Countries { get; set; }
    }
}
