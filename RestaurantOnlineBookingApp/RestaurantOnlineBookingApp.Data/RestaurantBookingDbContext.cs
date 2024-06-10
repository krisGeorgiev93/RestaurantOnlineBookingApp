
namespace RestaurantOnlineBookingApp.Data
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using RestaurantOnlineBookingApp.Data.Configurations;
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

        public DbSet<Photo> Photos { get; set; }
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
            builder.ApplyConfiguration(new RestaurantEntityConfiguration());
            base.OnModelCreating(builder);

            // Сийдване на ресторант и капацитет
            SeedData(builder);
        }
        private void SeedData(ModelBuilder builder)
        {
            // Сийдване на ресторанта
            var restaurants = new List<Restaurant>
            {
                new Restaurant
                {
                    Id = Guid.Parse("3614f373-2355-4e6c-96e5-542f0689752f"),
                    Name = "Restaurant Italy",
                    Address = "Georgi Georgiev 66",
                    Description = "Family restaurant with italian food",
                    StartingTime = new TimeSpan(12, 0, 0),
                    EndingTime = new TimeSpan(23, 45, 0),
                    ImageUrl = "https://www.japan-guide.com/g21/2036_family_01.jpg",
                    Capacity = 100,
                    CityId = 2,
                    CategoryId = 1,
                    OwnerId = Guid.Parse("44e7b2ef-dfa8-45ae-aca9-0b52b9a3df4d")
                }
            };

            builder.Entity<Restaurant>().HasData(restaurants);

            // Сийдване на капацитета отделно за всеки ден
            var capacities = CapacityPerDateSeeder.SeedCapacities(restaurants);
            builder.Entity<CapacityPerDate>().HasData(capacities);
        }
    }
}