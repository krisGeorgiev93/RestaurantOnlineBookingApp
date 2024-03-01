using Microsoft.EntityFrameworkCore;
using RestaurantOnlineBooking.Services.Data.Interfaces;
using RestaurantOnlineBookingApp.Data;
using RestaurantOnlineBookingApp.Data.Models;
using RestaurantOnlineBookingApp.Web.ViewModels.Meal;
using RestaurantOnlineBookingApp.Web.ViewModels.Restaurant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantOnlineBooking.Services.Data
{
    public class MealService : IMealService
    {
        private readonly RestaurantBookingDbContext dBContext;

        public MealService(RestaurantBookingDbContext dBContext)
        {
            this.dBContext = dBContext;
        }

        public async Task AddMealToRestaurantAsync(string restaurantId, MealFormViewModel mealViewModel)
        {
            if (!Guid.TryParse(restaurantId, out Guid restaurantGuid))
            {
                throw new ArgumentException("Invalid restaurantId");
            }

            var restaurant = await this.dBContext.Restaurants.FindAsync(restaurantGuid);

            if (restaurant == null)
            {
                throw new InvalidOperationException("Restaurant not found.");
            }

            var meal = new Meal
            {
                Name = mealViewModel.Name,
                Description = mealViewModel.Description,
                Price = mealViewModel.Price,
                ImageUrl = mealViewModel.ImageUrl,
                RestaurantId = restaurantGuid // Associate the meal with the restaurant
            };

            restaurant.Meals.Add(meal);
            await dBContext.SaveChangesAsync();
        }


        public async Task CreateAsync(MealFormViewModel mealFormViewModel)
        {
            Meal meal = new Meal()
            {
                Name = mealFormViewModel.Name,
                Description = mealFormViewModel.Description,
                Price = mealFormViewModel.Price,
                ImageUrl = mealFormViewModel.ImageUrl
            };

            await this.dBContext.Meals.AddAsync(meal);
            await this.dBContext.SaveChangesAsync();
        }

        public async Task DeleteMealAsync(string mealId)
        {
            var meal = await this.dBContext
                  .Meals.FirstAsync(m => m.Id.ToString() == mealId);

            if (meal == null)
            {
                throw new InvalidOperationException("Meal not found.");
            }

            this.dBContext.Meals.Remove(meal);

            await this.dBContext.SaveChangesAsync();
        }

        public async Task EditMealAsync(MealFormViewModel model)
        {
            var meal = await this.dBContext.Meals.FindAsync(model.Id);

            if (meal == null)
            {
                throw new InvalidOperationException("Meal not found.");
            }

            meal.Name = model.Name;
            meal.Description = model.Description;
            meal.Price = model.Price;
            meal.ImageUrl = model.ImageUrl;
            meal.RestaurantId = model.RestaurantId;

            await this.dBContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<MealAllViewModel>> GetAllMealsAsync()
        {
            var meals = await this.dBContext
                 .Meals
                .Select(m => new MealAllViewModel()
                {
                    Id = m.Id.ToString(),
                    Name = m.Name,
                    Description = m.Description,
                    Price = m.Price,
                    ImageUrl = m.ImageUrl,
                })
                .ToListAsync();

            return meals;
        }

        public async Task<IEnumerable<Meal>> GetAllMealsForRestaurantByIdAsync(string restaurantId)
        {
            return await this.dBContext
                .Meals
                .Where(m => m.RestaurantId.ToString() == restaurantId)
                .ToListAsync();
        }

        public async Task<MealFormViewModel> GetMealByIdAsync(string mealId)
        {
            var meal = await this.dBContext.Meals.FirstAsync(m => m.Id.ToString() == mealId);

            if (meal == null)
            {
                throw new InvalidOperationException("Meal not found.");
            }

            var mealForm = new MealFormViewModel()
            {
                Id = meal.Id,
                Name = meal.Name,
                Description = meal.Description,
                ImageUrl = meal.ImageUrl,
                Price = meal.Price,
                RestaurantId = (Guid)meal.RestaurantId
            };

            return mealForm;

        }

        public async Task<bool> MealExistsByIdAsync(string mealId)
        {
            bool IsExists = await this.dBContext
                .Meals
                .AnyAsync(r => r.Id.ToString() == mealId);

            return IsExists;
        }
    }
}
