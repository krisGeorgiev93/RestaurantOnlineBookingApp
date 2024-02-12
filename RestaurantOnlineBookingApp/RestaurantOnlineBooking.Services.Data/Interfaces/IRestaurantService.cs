using RestaurantOnlineBookingApp.Web.ViewModels.Home;
using RestaurantOnlineBookingApp.Web.ViewModels.Restaurant;

namespace RestaurantOnlineBooking.Services.Data.Interfaces
{
    public interface IRestaurantService
    {
        Task<IEnumerable<AllRestaurantsViewModel>> GetAllAsync();

        Task CreateRestaurantAsync(RestaurantFormModel model, string ownerId);

    }
}
