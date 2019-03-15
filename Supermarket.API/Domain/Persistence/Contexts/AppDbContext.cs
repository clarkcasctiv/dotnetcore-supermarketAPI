using Microsoft.EntityFrameworkCore;
using Supermarket.API.Domain.Models;

namespace Supermarket.API.Domain.Persistence.Contexts
{
    public class AppDbContext : DbContext
    {
        //private readonly IPasswordHasher passwordHasher;
        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Category>().ToTable("Categories");
            builder.Entity<Category>().HasKey(p => p.Id);
            builder.Entity<Category>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Category>().Property(p => p.Name).IsRequired().HasMaxLength(30);
            builder.Entity<Category>().HasMany(p => p.Products).WithOne(p => p.Category).HasForeignKey(p => p.CategoryId);

            builder.Entity<Category>().HasData(
                new Category { Id = 100, Name = "Fruits And Vegetables" }, // Id set manually due to in-memory provider
                new Category { Id = 101, Name = "Dairy" }
            );

            builder.Entity<Product>().ToTable("Products");
            builder.Entity<Product>().HasKey(p => p.Id);
            builder.Entity<Product>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Product>().Property(p => p.Name).IsRequired().HasMaxLength(30);
            builder.Entity<Product>().Property(p => p.Quantity).IsRequired();
            builder.Entity<Product>().Property(p => p.UnitOfMeasurement).IsRequired();

            builder.Entity<Product>().HasData(
               new Product
               {
                   Id = 100,
                   Name = "Apple",
                   Quantity = 1,
                   UnitOfMeasurement = EUnitOfMeasurement.Unity,
                   CategoryId = 100
               },
               new Product
               {
                   Id = 101,
                   Name = "Milk",
                   Quantity = 2,
                   UnitOfMeasurement = EUnitOfMeasurement.Liter,
                   CategoryId = 101
               }
           );

            //builder.Entity<User>().ToTable("Users");
            //builder.Entity<User>().HasKey(p => p.Id);
            //builder.Entity<User>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            //builder.Entity<User>().Property(p => p.Email).IsRequired().HasMaxLength(100);
            //builder.Entity<User>().Property(p => p.Password).IsRequired().HasMaxLength(100);

            //builder.Entity<User>().HasData(
            //    new User { Id = 1, Email = "admin@mail.com", Password = "ACObsBdohUONmrQVm94+VjO1rtHQ934BbnCsDztZOodctuaXEb+1gAoFmvjSQlirEg==" }, // Id set manually due to in-memory provider
            //    new User { Id = 2, Email = "user@mail.com", Password = "AGt9K+7F9PsSovlUc+ChGR7/oZPcJdH1tVB4I9WJjOpQM9LjI1c6mK3tbo38ZVin4A=" }
            //);

            //builder.Entity<Role>().ToTable("Roles");
            //builder.Entity<Role>().HasKey(p => p.Id);
            //builder.Entity<Role>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            //builder.Entity<Role>().Property(p => p.Name).IsRequired().HasMaxLength(50);

            //builder.Entity<Role>().HasData(
            // new Role { Id = 1, Name = ERole.Common.ToString() },
            // new Role { Id = 2, Name = ERole.Administrator.ToString() }
            //);

            //builder.Entity<User>().HasData(
            //    new User { Email = "admin@mail.com", Password = "ACObsBdohUONmrQVm94+VjO1rtHQ934BbnCsDztZOodctuaXEb+1gAoFmvjSQlirEg==" },
            //    new User { Email = "user@mail.com", Password = "AGt9K+7F9PsSovlUc+ChGR7/oZPcJdH1tVB4I9WJjOpQM9LjI1c6mK3tbo38ZVin4A=" }
            //);

            builder.Entity<UserRole>().HasKey(ur => new { ur.UserId, ur.RoleId });
        }
    }
}