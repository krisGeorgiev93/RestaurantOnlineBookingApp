using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantOnlineBookingApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantOnlineBookingApp.Data.Configurations
{
    internal class RestaurantEntityConfiguration : IEntityTypeConfiguration<Restaurant>
    {
        public void Configure(EntityTypeBuilder<Restaurant> builder)
        {
            builder.HasData(this.UploadRestaurants());

            builder.
                HasOne(r=> r.Category)
                .WithMany(c=> c.Restaurants)
                .HasForeignKey(r=> r.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(r=> r.Owner)
                .WithMany(o=> o.OwnedRestaurants)
                .HasForeignKey(r=> r.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
               .HasOne(r => r.City)
               .WithMany(o => o.Restaurants)
               .HasForeignKey(r => r.CityId)
               .OnDelete(DeleteBehavior.Restrict);


        }

        private Restaurant[] UploadRestaurants()
        {
            ICollection<Restaurant> restaurants = new HashSet<Restaurant>();
            Restaurant restaurant;

            restaurant = new Restaurant()
            {
                Name = "Asian Buffet",
                Address = "Ivan Vazov 26",
                Description = "Best food from Asia",
                ImageUrl = "https://cdn.vox-cdn.com/thumbor/Yb1U9a4hdQsC1iDQ_YIhJrqXL6g=/0x0:1024x682/1220x813/filters:focal(431x260:593x422):format(webp)/cdn.vox-cdn.com/uploads/chorus_image/image/59443047/makinoheader.0.jpg",
                Rating = 4,
                CityId = 1,
                CategoryId = 2,
                OwnerId = Guid.Parse("92bc2551-2565-4fc4-8ed6-98deffe533b9")
            };

            restaurants.Add(restaurant);

            restaurant = new Restaurant()
            {
                Name = "Best Of China",
                Address = "Hristo Botev 76",
                Description = "Best chinese in the country",
                ImageUrl = "https://www.opentable.co.uk/blog/wp-content/uploads/sites/110/2020/02/sweetmandarin1.jpg",
                Rating = 5,
                CityId = 1,
                CategoryId = 6,
                OwnerId = Guid.Parse("92bc2551-2565-4fc4-8ed6-98deffe533b9")
            };

            restaurants.Add(restaurant);

            return restaurants.ToArray();
        }
    }
}
