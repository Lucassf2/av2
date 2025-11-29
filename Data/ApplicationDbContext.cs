using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HamburgueriaBlazor.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Category { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ShoppingCart> ShoppingCart { get; set; }
        public DbSet<OrderHeader> OrderHeader { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Appetizer" },
                new Category { Id = 2, Name = "Entree" },
                new Category { Id = 3, Name = "Dessert" }
            );

            builder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "Spring Rolls",
                    Price = 5.99m,
                    Description = "Crispy vegetable spring rolls served with sweet chili sauce.",
                    SpecialTag = "Popular",
                    CategoryId = 1,
                    ImageUrl = "/images/product/spring_rolls.jpg"
                },
                new Product
                {
                    Id = 2,
                    Name = "Classic Burger",
                    Price = 12.99m,
                    Description = "Grilled beef burger with cheese, lettuce, tomato and pickles.",
                    SpecialTag = "Chef's Special",
                    CategoryId = 2,
                    ImageUrl = "/images/product/classic_burger.jpg"
                },
                new Product
                {
                    Id = 3,
                    Name = "New York Cheesecake",
                    Price = 7.99m,
                    Description = "Classic New York cheesecake with a graham cracker crust.",
                    SpecialTag = "Sweet Treat",
                    CategoryId = 3,
                    ImageUrl = "/images/product/cheesecake.jpg"
                }
            );
        }
    }
}
