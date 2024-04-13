using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantOnlineBookingApp.Data.Models;

namespace RestaurantOnlineBookingApp.Data.Configurations
{
    public class MealEntityConfiguration : IEntityTypeConfiguration<Meal>
    {
        public void Configure(EntityTypeBuilder<Meal> builder)
        {
            builder.HasData(this.UploadMeals());

            builder
            .HasOne(m => m.Restaurant)
            .WithMany(r => r.Meals)
            .HasForeignKey(m => m.RestaurantId)
            .OnDelete(DeleteBehavior.Cascade);
        }

        private Meal[] UploadMeals()
        {
            ICollection<Meal> meals = new HashSet<Meal>();

            Meal meal;

            meal = new Meal()
            {
                Id = 12,
                Name = "Pizza Peperoni",
                Description = "Italian pizza with cheese and peperoni",
                Price = 10.50m,
                ImageUrl = "https://www.simplyrecipes.com/thmb/KE6iMblr3R2Db6oE8HdyVsFSj2A=/1500x0/filters:no_upscale():max_bytes(150000):strip_icc()/__opt__aboutcom__coeus__resources__content_migration__simply_recipes__uploads__2019__09__easy-pepperoni-pizza-lead-3-1024x682-583b275444104ef189d693a64df625da.jpg",
                RestaurantId = Guid.Parse("3614f373-2355-4e6c-96e5-542f0689752f")

            };
            meals.Add(meal);         

            meal = new Meal()
            {
                Id = 14,
                Name = "Chicken Fried Rice",
                Description = "Chinese chicken fried rice with eggs",
                Price = 9.20m,
                ImageUrl = "https://tildaricelive.s3.eu-central-1.amazonaws.com/wp-content/uploads/2021/05/04111234/chicken-fried-rice-low-res-2.png",
                RestaurantId = Guid.Parse("3614f373-2355-4e6c-96e5-542f0689752f")

            };
            meals.Add(meal);


            return meals.ToArray();
        }
    }
}
