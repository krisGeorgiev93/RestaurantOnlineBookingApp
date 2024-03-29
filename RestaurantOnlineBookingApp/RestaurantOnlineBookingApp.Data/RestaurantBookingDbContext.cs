﻿
namespace RestaurantOnlineBookingApp.Data
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using RestaurantOnlineBookingApp.Data.Models;
    using System.Reflection;
    using System.Reflection.Emit;

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
        public DbSet<UserFavoritesRestaurants> UserFavoriteRestaurants { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<RestaurantGuest>()
                .HasKey(rg => new { rg.RestaurantId, rg.GuestId });

            builder.Entity<UserFavoritesRestaurants>()
       .HasKey(uf => new { uf.UserId, uf.RestaurantId });

            builder.Entity<Booking>()
          .HasOne(b => b.Restaurant)
          .WithMany(r => r.Bookings)
          .HasForeignKey(b => b.RestaurantId)
          .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<RestaurantGuest>()
            .HasOne(rg => rg.Restaurant)
            .WithMany(r => r.RestaurantGuests)
            .HasForeignKey(rg => rg.RestaurantId)
            .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Review>()
           .HasOne(r => r.Restaurant)
           .WithMany(r => r.Reviews)
           .HasForeignKey(r => r.RestaurantId)
           .OnDelete(DeleteBehavior.NoAction);


            builder.Entity<UserFavoritesRestaurants>()
           .HasOne(ufr => ufr.User)
           .WithMany()
           .HasForeignKey(ufr => ufr.UserId)
           .OnDelete(DeleteBehavior.NoAction); 

            builder.Entity<UserFavoritesRestaurants>()
                .HasOne(ufr => ufr.Restaurant)
                .WithMany()
                .HasForeignKey(ufr => ufr.RestaurantId)
                .OnDelete(DeleteBehavior.NoAction);

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