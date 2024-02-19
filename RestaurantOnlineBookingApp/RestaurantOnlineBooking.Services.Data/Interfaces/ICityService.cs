using RestaurantOnlineBookingApp.Web.ViewModels.Category;
using RestaurantOnlineBookingApp.Web.ViewModels.City;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantOnlineBooking.Services.Data.Interfaces
{
    public interface ICityService
    {
        Task<IEnumerable<SelectCityFormModel>> GetAllCitiesAsync();

        Task<IEnumerable<string>> AllCitiesNamesAsync();


    }
}
