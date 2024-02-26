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
    public class MealEntityConfiguration : IEntityTypeConfiguration<Meal>
    {
        public void Configure(EntityTypeBuilder<Meal> builder)
        {
            builder.HasData(this.UploadMeals());

            builder.Property(m => m.Price)
                .HasPrecision(18, 2);
        }

        private Meal[] UploadMeals()
        {
            ICollection<Meal> meals = new HashSet<Meal>();

            Meal meal;

            meal = new Meal()
            {
                Id = 11,
                Name = "Greek Salad",
                Description = "The best salad in Greece",
                Price = 2.50M,
                ImageUrl = "https://www.themediterraneandish.com/wp-content/uploads/2023/08/Greek-salad-web-story-poster-image.jpeg"
            };

            meals.Add(meal);

            meal = new Meal()
            {
                Id = 22,
                Name = "Shopska Salad",
                Description = "The best salad in Bulgaria",
                Price = 2.60M,
                ImageUrl = "https://www.craftbeering.com/wp-content/uploads/2020/07/Shopska-salad-Original-Bulgarian-cucumber-tomato-salad-1-720x720.jpg"
            };
            meals.Add(meal);

            return meals.ToArray();
        }
    }
}
