using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShepherdPie.Models;

namespace ShepherdPie.Data
{
    public class ShepherdPieDbContext : IdentityDbContext<User> 
    {
        private readonly IConfiguration _configuration;

        public ShepherdPieDbContext(DbContextOptions<ShepherdPieDbContext> options, IConfiguration config) 
            : base(options) 
        { 
            _configuration = config;
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<Condiment> Condiments { get; set; }
        public DbSet<PizzaCondiment> PizzaCondiments { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Call base to apply Identity configurations

            // Example of fluent API relationships (optional)
            modelBuilder.Entity<Order>()
                .HasMany(o => o.Pizzas)
                .WithOne()
                .HasForeignKey(p => p.OrderId);

            modelBuilder.Entity<Order>()
.HasOne(o => o.OrderTaker)
.WithMany(e => e.TakenOrders) // Employee navigation property
.HasForeignKey(o => o.OrderTakerEmployeeId);
// .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.DeliveryPerson)
                .WithMany(e => e.DeliveredOrders) // Employee navigation property
                .HasForeignKey(o => o.DeliveryPersonEmployeeId);
                // .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Pizza>()
    .HasOne(p => p.Order) // Each Pizza belongs to one Order
    .WithMany(o => o.Pizzas) // Each Order has many Pizzas
    .HasForeignKey(p => p.OrderId) // Explicitly link the foreign key (important)
    .OnDelete(DeleteBehavior.Cascade); // If the order is deleted, delete its pizzas too

    modelBuilder.Entity<Order>()
    .HasOne(o => o.User) // Navigation property in Order
    .WithMany(u => u.Orders) // Navigation property in User
    .HasForeignKey(o => o.UserId) // Foreign key in Order
    .OnDelete(DeleteBehavior.Cascade); // Cascade delete to remove orders when a user is deleted

modelBuilder.Entity<User>().HasData(
    new User 
    { 
        Id = "1", 
        FirstName = "John", 
        LastName = "Doe", 
        UserName = "johndoe", 
        Email = "johndoe@example.com", 
        NormalizedUserName = "JOHNDOE", 
        NormalizedEmail = "JOHNDOE@EXAMPLE.COM", 
        PasswordHash = "AQAAAAIAAYagAAAAEGwWZZErNlBxXJ1NNiYZW/J6DjR/7VXDm21IvJYB0aA==" // Pre-hashed password
    },
    new User 
    { 
        Id = "2", 
        FirstName = "Jane", 
        LastName = "Smith", 
        UserName = "janesmith", 
        Email = "janesmith@example.com", 
        NormalizedUserName = "JANESMITH", 
        NormalizedEmail = "JANESMITH@EXAMPLE.COM", 
        PasswordHash = "AQAAAAIAAYagAAAAEGwWZZErNlBxXJ1NNiYZW/J6DjR/7VXDm21IvJYB0aA==" 
    },
    new User 
    { 
        Id = "3", 
        FirstName = "Alice", 
        LastName = "Brown", 
        UserName = "alicebrown", 
        Email = "alicebrown@example.com", 
        NormalizedUserName = "ALICEBROWN", 
        NormalizedEmail = "ALICEBROWN@EXAMPLE.COM", 
        PasswordHash = "AQAAAAIAAYagAAAAEGwWZZErNlBxXJ1NNiYZW/J6DjR/7VXDm21IvJYB0aA==" 
    },
    new User 
    { 
        Id = "4", 
        FirstName = "Bob", 
        LastName = "Johnson", 
        UserName = "bobjohnson", 
        Email = "bobjohnson@example.com", 
        NormalizedUserName = "BOBJOHNSON", 
        NormalizedEmail = "BOBJOHNSON@EXAMPLE.COM", 
        PasswordHash = "AQAAAAIAAYagAAAAEGwWZZErNlBxXJ1NNiYZW/J6DjR/7VXDm21IvJYB0aA==" 
    }
);

            // Seed Orders
 // Seed Orders
// Seed Orders with explicit DateTimeKind for OrderDate
modelBuilder.Entity<Order>().HasData(
    new Order { OrderId = 1, OrderDate = DateTime.SpecifyKind(DateTime.Parse("2024-12-15T00:00:00Z"), DateTimeKind.Utc), TotalAmount = 50.00m, OrderStatus = "Completed", TipLeftCustomer = 5.00m, DeliveryFee = 5.00m, OrderTakerEmployeeId = 1, DeliveryPersonEmployeeId = 2, UserId = "2" },
    new Order { OrderId = 2, OrderDate = DateTime.SpecifyKind(DateTime.Parse("2024-12-16T00:00:00Z"), DateTimeKind.Utc), TotalAmount = 30.00m, OrderStatus = "Pending", TipLeftCustomer = 3.00m, DeliveryFee = 5.00m, OrderTakerEmployeeId = 2, DeliveryPersonEmployeeId = 3, UserId = "4"},
    new Order { OrderId = 3, OrderDate = DateTime.SpecifyKind(DateTime.Parse("2024-12-17T00:00:00Z"), DateTimeKind.Utc), TotalAmount = 40.00m, OrderStatus = "Completed", TipLeftCustomer = 4.00m, DeliveryFee = 5.00m, OrderTakerEmployeeId = 1, DeliveryPersonEmployeeId = 4, UserId = "3" },
    new Order { OrderId = 4, OrderDate = DateTime.SpecifyKind(DateTime.Parse("2024-12-18T00:00:00Z"), DateTimeKind.Utc), TotalAmount = 25.00m, OrderStatus = "Pending", TipLeftCustomer = 2.00m, DeliveryFee = 5.00m, OrderTakerEmployeeId = 3, DeliveryPersonEmployeeId = 2, UserId = "1" },
    new Order { OrderId = 5, OrderDate = DateTime.SpecifyKind(DateTime.Parse("2024-12-19T00:00:00Z"), DateTimeKind.Utc), TotalAmount = 35.00m, OrderStatus = "Pending", TipLeftCustomer = 3.50m, DeliveryFee = 5.00m, OrderTakerEmployeeId = 2, DeliveryPersonEmployeeId = 1, UserId = "2" },
    new Order { OrderId = 6, OrderDate = DateTime.SpecifyKind(DateTime.Parse("2024-12-20T00:00:00Z"), DateTimeKind.Utc), TotalAmount = 55.00m, OrderStatus = "Pending", TipLeftCustomer = 5.50m, DeliveryFee = 5.00m, OrderTakerEmployeeId = 1, DeliveryPersonEmployeeId = 4, UserId = "4" },
    new Order { OrderId = 7, OrderDate = DateTime.SpecifyKind(DateTime.Parse("2024-12-20T12:00:00Z"), DateTimeKind.Utc), TotalAmount = 20.00m, OrderStatus = "Pending", TipLeftCustomer = 1.50m, DeliveryFee = 5.00m, OrderTakerEmployeeId = 3, DeliveryPersonEmployeeId = 2, UserId = "2" }
);






    // Seed Pizzas
    modelBuilder.Entity<Pizza>().HasData(
        new Pizza { PizzaId = 1, OrderId = 1, Size = "Large", BasePrice = 15.00m },
        new Pizza { PizzaId = 2, OrderId = 1, Size = "Medium", BasePrice = 12.00m },
        new Pizza { PizzaId = 3, OrderId = 2, Size = "Small", BasePrice = 10.00m },
        new Pizza { PizzaId = 4, OrderId = 3, Size = "Large", BasePrice = 15.00m },
        new Pizza { PizzaId = 5, OrderId = 4, Size = "Medium", BasePrice = 12.00m },
        new Pizza { PizzaId = 6, OrderId = 5, Size = "Small", BasePrice = 10.00m },
        new Pizza { PizzaId = 7, OrderId = 6, Size = "Large", BasePrice = 15.00m },
        new Pizza { PizzaId = 8, OrderId = 7, Size = "Medium", BasePrice = 12.00m }
    );

    // Seed Condiments
  modelBuilder.Entity<Condiment>().HasData(
    // Sauces
    new Condiment { CondimentId = 1, CondimentName = "Red Sauce", CondimentType = "Sauce", Cost = 0.50m },
    new Condiment { CondimentId = 2, CondimentName = "Garlic White Sauce", CondimentType = "Sauce", Cost = 0.75m },
    
    // Meats
    new Condiment { CondimentId = 3, CondimentName = "Sausage", CondimentType = "Meat", Cost = 1.50m },
    new Condiment { CondimentId = 4, CondimentName = "Pepperoni", CondimentType = "Meat", Cost = 1.75m },
    
    // Cheeses
    new Condiment { CondimentId = 5, CondimentName = "Buffalo Mozzarella", CondimentType = "Cheese", Cost = 1.25m },
    new Condiment { CondimentId = 6, CondimentName = "Parmesan", CondimentType = "Cheese", Cost = 1.50m },
    
    // Vegetable Toppings
    new Condiment { CondimentId = 7, CondimentName = "Mushroom", CondimentType = "Vegetable Topping", Cost = 0.75m },
    new Condiment { CondimentId = 8, CondimentName = "Jalapeno", CondimentType = "Vegetable Topping", Cost = 0.50m },
    new Condiment { CondimentId = 9, CondimentName = "Green Pepper", CondimentType = "Vegetable Topping", Cost = 0.50m },
    new Condiment { CondimentId = 10, CondimentName = "Black Olive", CondimentType = "Vegetable Topping", Cost = 0.75m }
);


    // Seed Employees
    modelBuilder.Entity<Employee>().HasData(
        new Employee { EmployeeId = 1, Name = "Alice" },
        new Employee { EmployeeId = 2, Name = "Bob" },
        new Employee { EmployeeId = 3, Name = "Charlie" },
        new Employee { EmployeeId = 4, Name = "Diana" },
        new Employee { EmployeeId = 5, Name = "Edward" }
    );

    // Seed PizzaCondiment (Join Table)
    modelBuilder.Entity<PizzaCondiment>().HasData(
        new PizzaCondiment { PizzaCondimentId = 1, PizzaId = 1, CondimentId = 1 },
        new PizzaCondiment { PizzaCondimentId = 2, PizzaId = 1, CondimentId = 2 },
        new PizzaCondiment { PizzaCondimentId = 3, PizzaId = 2, CondimentId = 3 },
        new PizzaCondiment { PizzaCondimentId = 4, PizzaId = 2, CondimentId = 4 },
        new PizzaCondiment { PizzaCondimentId = 5, PizzaId = 3, CondimentId = 5 },
        new PizzaCondiment { PizzaCondimentId = 6, PizzaId = 4, CondimentId = 6 },
        new PizzaCondiment { PizzaCondimentId = 7, PizzaId = 5, CondimentId = 7 },
        new PizzaCondiment { PizzaCondimentId = 8, PizzaId = 6, CondimentId = 8 },
        new PizzaCondiment { PizzaCondimentId = 9, PizzaId = 7, CondimentId = 1 },
        new PizzaCondiment { PizzaCondimentId = 10, PizzaId = 8, CondimentId = 2 }
    );
        }
    }
}
