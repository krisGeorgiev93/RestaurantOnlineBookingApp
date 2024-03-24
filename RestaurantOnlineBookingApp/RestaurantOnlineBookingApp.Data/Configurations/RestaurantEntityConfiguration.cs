using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantOnlineBookingApp.Data.Models;
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

            builder.
                HasOne(r => r.Category)
                .WithMany(c => c.Restaurants)
                .HasForeignKey(r => r.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(r => r.Owner)
                .WithMany(o => o.OwnedRestaurants)
                .HasForeignKey(r => r.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
               .HasOne(r => r.City)
               .WithMany(o => o.Restaurants)
               .HasForeignKey(r => r.CityId)
               .OnDelete(DeleteBehavior.Restrict);

            //when a Restaurant entity is deleted,
            //all related Meal entities should also be deleted(OnDelete(DeleteBehavior.Cascade)).
            //builder
            //  .HasMany(r => r.Meals)
            //  .WithOne(m => m.Restaurant)
            //  .HasForeignKey(m => m.RestaurantId)
            //  .OnDelete(DeleteBehavior.Cascade);
         
        }
        private Restaurant[] UploadRestaurants()
        {
            ICollection<Restaurant> restaurants = new HashSet<Restaurant>();
            Restaurant restaurant;

            restaurant = new Restaurant()
            {
                Name = "Turkish Restaurant",
                Address = "Georgi Ivanov 26",
                Description = "Best food from Turkey",
                StartingTime = new TimeSpan(12, 0, 0),
                EndingTime = new TimeSpan(23, 30, 0),
                ImageUrl = "https://images.squarespace-cdn.com/content/v1/62f4e2e25f95bb5d35522adc/0c8e42d8-aa26-4c9e-acbc-bcfd336b3731/Havin_a_Turkish_X_socialawakening+18.jpg",
                Capacity = 135,
                CityId = 3,
                CategoryId = 4,
                OwnerId = Guid.Parse("C4F8569C-1CDA-4B0B-94E4-16B44A4631CF")
            };

            restaurants.Add(restaurant);
            restaurant = new Restaurant()
            {
                Name = "Asian Buffet",
                Address = "Ivan Ivanov 26",
                Description = "Best food from Asia",
                StartingTime = new TimeSpan(17, 0, 0),
                EndingTime = new TimeSpan(23, 30, 0),
                ImageUrl = "https://cdn.vox-cdn.com/thumbor/Yb1U9a4hdQsC1iDQ_YIhJrqXL6g=/0x0:1024x682/1220x813/filters:focal(431x260:593x422):format(webp)/cdn.vox-cdn.com/uploads/chorus_image/image/59443047/makinoheader.0.jpg",
                Capacity = 100,
                CityId = 1,
                CategoryId = 2,
                OwnerId = Guid.Parse("C4F8569C-1CDA-4B0B-94E4-16B44A4631CF")
            };

            restaurants.Add(restaurant);

            restaurant = new Restaurant()
            {
                Name = "Best Of China",
                Address = "Hristo Botev 76",
                Description = "Best chinese in the country",
                StartingTime = new TimeSpan(18, 0, 0),
                EndingTime = new TimeSpan(23, 0, 0),
                ImageUrl = "https://www.opentable.co.uk/blog/wp-content/uploads/sites/110/2020/02/sweetmandarin1.jpg",
                Capacity = 50,
                CityId = 1,
                CategoryId = 6,
                OwnerId = Guid.Parse("C4F8569C-1CDA-4B0B-94E4-16B44A4631CF")
            };

            restaurants.Add(restaurant);

            restaurant = new Restaurant()
            {
                Name = "Bulgarian Taste",
                Address = "Hristo Hristov 74",
                Description = "Traditional food from Bulgarian Kitchen",
                StartingTime = new TimeSpan(12, 0, 0),
                EndingTime = new TimeSpan(23, 45, 0),
                ImageUrl = "https://media-cdn.tripadvisor.com/media/photo-s/03/b7/8b/ed/chevermeto-traditional.jpg",
                Capacity = 150,
                CityId = 2,
                CategoryId = 1,
                OwnerId = Guid.Parse("C4F8569C-1CDA-4B0B-94E4-16B44A4631CF")
            };

            restaurants.Add(restaurant);           

            return restaurants.ToArray();
        }
       
    }
}
