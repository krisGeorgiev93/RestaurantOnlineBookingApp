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
    }
}
