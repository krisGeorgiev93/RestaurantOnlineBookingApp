﻿
namespace RestaurantOnlineBookingApp.Data
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using RestaurantOnlineBookingApp.Data.Models;
    using System.Reflection;

    public class RestaurantBookingDbContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>
    {
        public RestaurantBookingDbContext(DbContextOptions<RestaurantBookingDbContext> options)
            : base(options)
        {
        }

        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<CapacityPerDate> CapacitiesParDate { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<RestaurantGuest>()
                .HasKey(rg => new { rg.RestaurantId, rg.GuestId });

            Assembly assembly = Assembly.GetAssembly(typeof(RestaurantBookingDbContext))
                ?? Assembly.GetExecutingAssembly();
            builder.ApplyConfigurationsFromAssembly(assembly);

            base.OnModelCreating(builder);
        }

        public void SeedAdmin(UserManager<AppUser> userManager, RoleManager<IdentityRole<Guid>> roleManager)
        {
            SeedRoles(roleManager);
            SeedAdminUser(userManager);
        }

        private void SeedRoles(RoleManager<IdentityRole<Guid>> roleManager)
        {
            // Check if Admin role exists, if not, create it
            if (!roleManager.RoleExistsAsync("Admin").Result)
            {
                var role = new IdentityRole<Guid>("Admin");
                roleManager.CreateAsync(role).Wait();
            }
        }

        private void SeedAdminUser(UserManager<AppUser> userManager)
        {
            // Check if the admin user already exists
            if (userManager.FindByEmailAsync("admin@example.com").Result == null)
            {
                // Create a new admin user
                var user = new AppUser
                {
                    UserName = "Admin",
                    Email = "admin@test.com",
                    FirstName = "Admin",
                    LastName = "Test"
                };

                // Add the admin user with a predefined password
                var result = userManager.CreateAsync(user, "123456").Result;

                if (result.Succeeded)
                {
                    // Assign the Admin role to the admin user
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }
        }
    }
}