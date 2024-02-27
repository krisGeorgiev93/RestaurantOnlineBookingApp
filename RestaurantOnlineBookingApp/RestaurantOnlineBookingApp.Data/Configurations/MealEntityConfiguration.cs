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
        }

        private Meal[] UploadMeals()
        {
            ICollection<Meal> meals = new HashSet<Meal>();

            Meal meal;

            meal = new Meal()
            {
                Id = 1,
                Name = "Pizza Peperoni",
                Description = "Italian pizza with cheese and peperoni",
                Price = 10.50,
                ImageUrl = "https://www.simplyrecipes.com/thmb/KE6iMblr3R2Db6oE8HdyVsFSj2A=/1500x0/filters:no_upscale():max_bytes(150000):strip_icc()/__opt__aboutcom__coeus__resources__content_migration__simply_recipes__uploads__2019__09__easy-pepperoni-pizza-lead-3-1024x682-583b275444104ef189d693a64df625da.jpg",
                RestaurantId = Guid.Parse("1604F79D-C4A9-4413-9708-76A07686370D")
            };
            meals.Add(meal);

            meal = new Meal()
            {
                Id = 2,
                Name = "Chicken Noodles",
                Description = "Chinese chicken noodles with eggs and onion",
                Price = 7.50,
                ImageUrl = "https://sinfullyspicy.com/wp-content/uploads/2023/01/1200-by-1200-images-5-500x375.jpg",
                RestaurantId = Guid.Parse("1604F79D-C4A9-4413-9708-76A07686370D")
            };
            meals.Add(meal);

            meal = new Meal()
            {

                Id = 3,
                Name = "Chicken Fried Rice",
                Description = "Chinese chicken fried rice with eggs",
                Price = 9.20,
                ImageUrl = "https://tildaricelive.s3.eu-central-1.amazonaws.com/wp-content/uploads/2021/05/04111234/chicken-fried-rice-low-res-2.png",
                RestaurantId = Guid.Parse("1604F79D-C4A9-4413-9708-76A07686370D")
            };
            meals.Add(meal);


            return meals.ToArray();
        }
    }
}
