using RestaurantOnlineBookingApp.Data.Models;
using RestaurantOnlineBookingApp.Web.ViewModels.Meal;
using RestaurantOnlineBookingApp.Web.ViewModels.Restaurant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantOnlineBooking.Services.Data.Interfaces
{
    public interface IMealService
    {
       Task CreateAsync(MealFormModel model);

       Task<IEnumerable<MealAllViewModel>> GetAllMealsAsync();


    }
}
