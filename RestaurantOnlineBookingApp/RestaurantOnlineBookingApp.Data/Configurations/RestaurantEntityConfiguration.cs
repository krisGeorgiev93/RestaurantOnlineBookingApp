using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantOnlineBookingApp.Data.Models;
using System.Reflection.Emit;

namespace RestaurantOnlineBookingApp.Data.Configurations
{
    internal class RestaurantEntityConfiguration : IEntityTypeConfiguration<Restaurant>
    {
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

            return restaurants.ToArray();
        }
    }
}
