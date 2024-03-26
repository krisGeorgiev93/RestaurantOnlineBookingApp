using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantOnlineBookingApp.Data.Models;
using System.Reflection.Emit;
namespace RestaurantOnlineBookingApp.Data.Configurations
{
    public class RestaurantEntityConfiguration : IEntityTypeConfiguration<Restaurant>
    {
        private readonly RestaurantBookingDbContext _context;

        public RestaurantEntityConfiguration(RestaurantBookingDbContext context)
        {
            _context = context;
        }
        public void Configure(EntityTypeBuilder<Restaurant> builder)
        {
            builder.HasData(this.UploadRestaurants());

            builder.Property(r => r.IsActive)
                .HasDefaultValue(true);

            builder
                    .HasMany(r => r.CapacityPerDates)
                    .WithOne(c => c.Restaurant)
                    .HasForeignKey(c => c.RestaurantId)
                    .OnDelete(DeleteBehavior.NoAction);

            builder.
                HasOne(r => r.Category)
                .WithMany(c => c.Restaurants)
                .HasForeignKey(r => r.CategoryId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(r => r.Owner)
                .WithMany(o => o.OwnedRestaurants)
                .HasForeignKey(r => r.OwnerId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
               .HasOne(r => r.City)
               .WithMany(o => o.Restaurants)
               .HasForeignKey(r => r.CityId)
               .OnDelete(DeleteBehavior.NoAction);

            builder
             .HasMany(r => r.Meals)
             .WithOne(m => m.Restaurant)
             .HasForeignKey(m => m.RestaurantId)
             .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(r => r.Bookings)
            .WithOne(b => b.Restaurant)
            .HasForeignKey(b => b.RestaurantId)
             .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(r => r.Reviews)
            .WithOne(b => b.Restaurant)
            .HasForeignKey(b => b.RestaurantId)
             .OnDelete(DeleteBehavior.NoAction);


            // Seed capacities after seeding restaurants
            SeedCapacities();

        }
        private void SeedCapacities()
        {
            foreach (var restaurant in UploadRestaurants())
            {
                int capacityIdCounter = 1;
                // Generate capacities for the next 60 days for each restaurant
                for (int i = 0; i < 60; i++)
                {
                    var date = DateTime.Now.Date.AddDays(i);
                    var capacity = new CapacityPerDate
                    {
                        Id = capacityIdCounter++,
                        RestaurantId = restaurant.Id,
                        Date = date,
                        Capacity = restaurant.Capacity
                    };

                    _context.CapacitiesParDate.Add(capacity);
                }
            }
            _context.SaveChanges();
        }
        private Restaurant[] UploadRestaurants()
        {
            ICollection<Restaurant> restaurants = new HashSet<Restaurant>();
            Restaurant restaurant;

            restaurant = new Restaurant()
            {
                Name = "Restaurant Mari",
                Address = "Georgi Georgiev 66",
                Description = "Family restaurant with italian food",
                StartingTime = new TimeSpan(12, 0, 0),
                EndingTime = new TimeSpan(23, 45, 0),
                ImageUrl = "https://www.japan-guide.com/g21/2036_family_01.jpg",
                Capacity = 100,
                CityId = 2,
                CategoryId = 1,
                OwnerId = Guid.Parse("C4F8569C-1CDA-4B0B-94E4-16B44A4631CF")
            };
            restaurants.Add(restaurant);

            return restaurants.ToArray();
        }

    }
}
