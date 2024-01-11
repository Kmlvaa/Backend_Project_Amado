using Backend_Project_Amado.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Backend_Project_Amado.Data
{
    public class AppDbContext: IdentityDbContext<AppUser>
    {
        public AppDbContext()
        {
        }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Checkout> Checkouts { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<ContactInfo> Contacts { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Images> Images { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductColor> ProductsColor { get; set; }
        public DbSet<ProductImage> ProductsImage { get; set; }
        public DbSet<Subscriber> Subscribers { get; set; }
    }
}
