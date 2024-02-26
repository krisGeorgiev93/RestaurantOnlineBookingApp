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
            builder.HasData(
                 new MenuMeal { MenuId = 1, MealId = 11 }, // MenuId 1 contains MealId 1
                 new MenuMeal { MenuId = 2, MealId = 22 }  // MenuId 2 contains MealId 2
             );
        }
    }
}
