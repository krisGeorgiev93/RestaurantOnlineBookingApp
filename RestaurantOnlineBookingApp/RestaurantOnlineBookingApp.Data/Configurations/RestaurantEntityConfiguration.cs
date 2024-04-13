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
    public class RestaurantEntityConfiguration : IEntityTypeConfiguration<Restaurant>
    {
        public void Configure(EntityTypeBuilder<Restaurant> builder)
        {          

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

        }
    }
}
