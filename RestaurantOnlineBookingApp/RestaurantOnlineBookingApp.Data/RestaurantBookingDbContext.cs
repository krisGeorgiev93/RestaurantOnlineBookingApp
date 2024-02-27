
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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<RestaurantGuest>()
                .HasKey(rg=> new { rg.RestaurantId, rg.GuestId });

            Assembly assembly = Assembly.GetAssembly(typeof(RestaurantBookingDbContext))
                ?? Assembly.GetExecutingAssembly();
            builder.ApplyConfigurationsFromAssembly(assembly);

            base.OnModelCreating(builder);
        }
    }
}
