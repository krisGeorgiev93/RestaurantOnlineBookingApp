using RestaurantOnlineBookingApp.Web.ViewModels.Home;

namespace RestaurantOnlineBooking.Services.Data.Interfaces
{
    public interface IRestaurantService
    {
        Task<IEnumerable<AllRestaurantsViewModel>> GetAllAsync();


    }
}
