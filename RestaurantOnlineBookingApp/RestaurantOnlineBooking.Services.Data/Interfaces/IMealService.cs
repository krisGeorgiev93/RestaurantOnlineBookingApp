using RestaurantOnlineBookingApp.Data.Models;
using RestaurantOnlineBookingApp.Web.ViewModels.Meal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantOnlineBooking.Services.Data.Interfaces
{
    public interface IMealService
    {

        Task CreateAsync(MealFormViewModel mealFormViewModel);

        Task<IEnumerable<MealAllViewModel>> GetAllMealsAsync();

        Task AddMealToRestaurantAsync(string restaurantId, MealFormViewModel mealViewModel);

        Task<IEnumerable<Meal>> GetAllMealsForRestaurantByIdAsync(string restaurantId);


    }
}
