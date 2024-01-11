using Microsoft.AspNetCore.Identity;

namespace Backend_Project_Amado.Entities
{
    public class AppUser: IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
