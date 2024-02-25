using RestaurantOnlineBooking.Services.Data.Interfaces;
using RestaurantOnlineBookingApp.Data;
using RestaurantOnlineBookingApp.Data.Models;
using RestaurantOnlineBookingApp.Web.ViewModels.Meal;
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

        public async Task Create(MealFormModel model)
        {
            Meal meal = new Meal()
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
            };

            await this._dbContext.Meals.AddAsync(meal);
            await this._dbContext.SaveChangesAsync();

        }
    }
}
