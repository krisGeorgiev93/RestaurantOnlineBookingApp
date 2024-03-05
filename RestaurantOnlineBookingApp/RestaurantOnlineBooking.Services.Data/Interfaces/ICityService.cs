using RestaurantOnlineBookingApp.Web.ViewModels.City;

namespace RestaurantOnlineBooking.Services.Data.Interfaces
{
    public interface ICityService
    {
        Task<IEnumerable<SelectCityFormModel>> GetAllCitiesAsync();

        Task<IEnumerable<string>> AllCitiesNamesAsync();


    }
}
