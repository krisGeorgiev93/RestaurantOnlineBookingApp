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
        private readonly RestaurantBookingDbContext _dbContext;

        public MealService(RestaurantBookingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateAsync(MealFormModel model)
        {
            Meal meal = new Meal()
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                ImageUrl = model.ImageUrl
            };

            await this._dbContext.Meals.AddAsync(meal);
            await this._dbContext.SaveChangesAsync();

        }

        public async Task<IEnumerable<MealAllViewModel>> GetAllMealsAsync()
        {
            var meals = await this._dbContext
                .Meals
               .Select(m => new MealAllViewModel()
               {
                   Id = m.Id.ToString(),
                   Name = m.Name,
                   Description = m.Description,
                   Price = m.Price.ToString(),
                   ImageUrl = m.ImageUrl,
               })
               .ToListAsync();

            return meals;
        }
    }
}
