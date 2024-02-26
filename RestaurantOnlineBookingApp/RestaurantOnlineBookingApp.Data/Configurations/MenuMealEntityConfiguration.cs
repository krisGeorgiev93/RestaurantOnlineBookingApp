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
    public class MenuMealEntityConfiguration : IEntityTypeConfiguration<MenuMeal>
    {
        public void Configure(EntityTypeBuilder<MenuMeal> builder)
        {
            builder.HasData(this.UploadMenuMeal());
        }

        private MenuMeal[] UploadMenuMeal()
        {
            ICollection<MenuMeal> menuMeals = new HashSet<MenuMeal>();

            MenuMeal menuMeal;

            menuMeal = new MenuMeal()
            {
                MenuId = 1,
                MealId = 11
            };
            menuMeals.Add(menuMeal);

            menuMeal = new MenuMeal()
            {
                MenuId = 2,
                MealId = 22
            };
            menuMeals.Add(menuMeal);

            return menuMeals.ToArray();
        }
    }
}
