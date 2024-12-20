using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ShepardPies.Models;
using Microsoft.AspNetCore.Identity;

namespace ShepardPies.Data;
public class ShepardPiesDbContext : IdentityDbContext<IdentityUser>
{
    private readonly IConfiguration _configuration;
    public DbSet<UserProfile> UserProfiles { get; set; }
    public DbSet<Pizza> Pizzas { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Sauce> Sauces { get; set; }
    public DbSet<Cheese> Cheeses { get; set; }
    public DbSet<Topping> Toppings { get; set; }
    public DbSet<PizzaTopping> PizzaToppings { get; set; }



    public ShepardPiesDbContext(DbContextOptions<ShepardPiesDbContext> context, IConfiguration config) : base(context)
    {
        _configuration = config;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole []
        {
            new IdentityRole
            {
                Id = "c3aaeb97-d2ba-4a53-a521-4eea61e59b35",
                Name = "Admin",
                NormalizedName = "admin"
            },
            new IdentityRole
            {
                Id = "c4bbeb97-d3ba-4b53-b521-5ffa61e59b36",
                Name = "Employee",
                NormalizedName = "employee"
            }
        });

        modelBuilder.Entity<IdentityUser>().HasData(new IdentityUser []
        {
            new IdentityUser
            {
                Id = "dbc40bc6-0829-4ac5-a3ed-180f5e916a5f",
                UserName = "Administrator",
                Email = "admina@strator.comx",
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, _configuration["AdminPassword"])
            },
            new IdentityUser
            {
                Id = "d6bc2e97-e1fc-4a2f-b112-6ff861e69c37",
                UserName = "employee1",
                Email = "employee1@company.com",
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, "Employee1")
            },

            new IdentityUser
            {
                Id = "a1bc2e97-e2fc-4a3f-b123-7ff861e69d38",
                UserName = "employee2",
                Email = "employee2@company.com",
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, "Employee2")
            },
            new IdentityUser
            {
                Id = "b2cd3e97-f3dc-4b4f-b234-8ff861e69e39",
                UserName = "employee3",
                Email = "employee3@company.com",
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, "Employee3")
            },
            new IdentityUser
            {
                Id = "c3de4e97-g4ec-4c5f-b345-9ff861e69f40",
                UserName = "employee4",
                Email = "employee4@company.com",
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, "Employee4")
            },
            new IdentityUser
            {
                Id = "d4ef5e97-h5fc-4d6f-b456-aff861e69g41",
                UserName = "employee5",
                Email = "employee5@company.com",
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, "Employee5")
            }
            
        });


        modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>[]
        {
            new IdentityUserRole<string>
            {
                RoleId = "c3aaeb97-d2ba-4a53-a521-4eea61e59b35", // Admin role ID
                UserId = "dbc40bc6-0829-4ac5-a3ed-180f5e916a5f"  // User ID for Admin
            },
            new IdentityUserRole<string>
            {
                RoleId = "c4bbeb97-d3ba-4b53-b521-5ffa61e59b36", // Employee role ID
                UserId = "d6bc2e97-e1fc-4a2f-b112-6ff861e69c37"  // User ID for Employee
            },
             new IdentityUserRole<string>
            {
                RoleId = "c4bbeb97-d3ba-4b53-b521-5ffa61e59b36", // Employee role ID
                UserId = "a1bc2e97-e2fc-4a3f-b123-7ff861e69d38"  // Employee 2
            },
            new IdentityUserRole<string>
            {
                 RoleId = "c4bbeb97-d3ba-4b53-b521-5ffa61e59b36", // Employee role ID
                UserId = "b2cd3e97-f3dc-4b4f-b234-8ff861e69e39"  // Employee 3
            },
            new IdentityUserRole<string>
            {
                RoleId = "c4bbeb97-d3ba-4b53-b521-5ffa61e59b36", // Employee role ID
                UserId = "c3de4e97-g4ec-4c5f-b345-9ff861e69f40"  // Employee 4
            },
            new IdentityUserRole<string>
            {
                RoleId = "c4bbeb97-d3ba-4b53-b521-5ffa61e59b36", // Employee role ID
                UserId = "d4ef5e97-h5fc-4d6f-b456-aff861e69g41"  // Employee 5
            }
        });

        
        modelBuilder.Entity<UserProfile>().HasData(new UserProfile []
        {
            new UserProfile
            {
                Id = 1,
                IdentityUserId = "dbc40bc6-0829-4ac5-a3ed-180f5e916a5f",
                FirstName = "Admina",
                LastName = "Strator",
                Address = "101 Main Street",
            },
              new UserProfile
              {
                Id = 2,
                IdentityUserId = "d6bc2e97-e1fc-4a2f-b112-6ff861e69c37",
                FirstName = "Tom",
                LastName = "Jones",
                Address = " 220 High Street",
            },
             new UserProfile
            {
                Id = 3,
                IdentityUserId = "a1bc2e97-e2fc-4a3f-b123-7ff861e69d38",
                FirstName = "Emily",
                LastName = "Davis",
                Address = "123 Oak Street"
            },
            new UserProfile
            {
                Id = 4,
                IdentityUserId = "b2cd3e97-f3dc-4b4f-b234-8ff861e69e39",
                FirstName = "Michael",
                LastName = "Smith",
                Address = "456 Maple Avenue"
            },
            new UserProfile
            {
                Id = 5,
                IdentityUserId = "c3de4e97-g4ec-4c5f-b345-9ff861e69f40",
                FirstName = "Sarah",
                LastName = "Johnson",
                Address = "789 Pine Lane"
            },
            new UserProfile
            {
                Id = 6,
                IdentityUserId = "d4ef5e97-h5fc-4d6f-b456-aff861e69g41",
                FirstName = "Robert",
                LastName = "Brown",
                Address = "321 Elm Road"
            }
            
        });

        modelBuilder.Entity<Pizza>().HasData(new Pizza[] 
        {
            new Pizza { Id = 1, Size = "Small", Price = 10.00m, CheeseId = 1, SauceId = 1, OrderId = 1 },
            new Pizza { Id = 2, Size = "Medium", Price = 12.00m, CheeseId = 2, SauceId = 2, OrderId = 1 },
            new Pizza { Id = 3, Size = "Large", Price = 15.00m, CheeseId = 3, SauceId = 3, OrderId = 2 },
            new Pizza { Id = 4, Size = "Large", Price = 15.00m, CheeseId = 2, SauceId = 1, OrderId = 3 }
        });


        modelBuilder.Entity<Cheese>().HasData(new Cheese[]
        {
            new Cheese { Id = 1, Name = "Buffalo Mozzarella" },
            new Cheese { Id = 2, Name = "Four Cheese" },
            new Cheese { Id = 3, Name = "Vegan" },
            new Cheese { Id = 4, Name = "None" } // Cheeseless option
        });


        modelBuilder.Entity<Sauce>().HasData(new Sauce[]
        {
            new Sauce { Id = 1, Name = "Marinara" },
            new Sauce { Id = 2, Name = "Arrabbiata" },
            new Sauce { Id = 3, Name = "Garlic White" },
            new Sauce { Id = 4, Name = "None" } // Sauceless pizza option
        });


        modelBuilder.Entity<Topping>().HasData(new Topping[]
        {
            new Topping { Id = 1, Name = "Sausage", Price = 0.50m, },
            new Topping { Id = 2, Name = "Pepperoni", Price = 0.50m,},
            new Topping { Id = 3, Name = "Mushroom", Price = 0.50m,},
            new Topping { Id = 4, Name = "Onion", Price = 0.50m,},
            new Topping { Id = 5, Name = "Green Pepper", Price = 0.50m,},
            new Topping { Id = 6, Name = "Black Olive", Price = 0.50m,},
            new Topping { Id = 7, Name = "Basil", Price = 0.50m,  },
            new Topping { Id = 8, Name = "Extra Cheese", Price = 0.50m, }
        });


        modelBuilder.Entity<Order>().HasData(new Order[]
        {
            new Order { Id = 1, OrderPlacedOn = new DateTime(2024, 12, 15, 14, 30, 0), UserProfileId = 2, DriverId = null, Tip = 5.00m }, 
            new Order { Id = 2, OrderPlacedOn = new DateTime(2024, 12, 16, 12, 0, 0), UserProfileId = 2, DriverId = 4, Tip = null }, 
            new Order { Id = 3, OrderPlacedOn = new DateTime(2024, 12, 17, 18, 45, 0), UserProfileId = 3, DriverId = 5, Tip = 7.00m }
        });

        modelBuilder.Entity<PizzaTopping>().HasData(new PizzaTopping[]
        {
            new PizzaTopping {Id = 1, PizzaId = 1, ToppingId = 1 }, // Sausage on Pizza 1
            new PizzaTopping {Id = 2, PizzaId = 1, ToppingId = 2 }, // Pepperoni on Pizza 1
            new PizzaTopping {Id = 3, PizzaId = 2, ToppingId = 3 }, // Mushroom on Pizza 2
            new PizzaTopping {Id = 4, PizzaId = 2, ToppingId = 4 }, // Onion on Pizza 2
            new PizzaTopping { Id = 5, PizzaId = 3, ToppingId = 5 }, // Green Pepper on Pizza 3
            new PizzaTopping {Id = 6,  PizzaId = 4, ToppingId = 6 }, // Black Olive on Pizza 4
        });


        
    }
}