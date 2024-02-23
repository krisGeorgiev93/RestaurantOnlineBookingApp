using RestaurantOnlineBooking.Services.Data.Models;
using RestaurantOnlineBookingApp.Web.ViewModels.Home;
using RestaurantOnlineBookingApp.Web.ViewModels.Restaurant;

namespace RestaurantOnlineBooking.Services.Data.Interfaces
{
    public interface IRestaurantService
    {
        Task<IEnumerable<AllRestaurantsViewModel>> GetAllAsync();

        Task<string> CreateAndReturnRestaurantIdAsync(RestaurantFormModel model, string ownerId);

        Task<AllRestaurantsFilteredServiceModel> AllAsync(AllRestaurantsQueryModel model);

        Task<IEnumerable<RestaurantAllViewModel>> AllByOwnerIdAsync(string ownerId);

        Task<IEnumerable<RestaurantAllViewModel>> AllByUserIdAsync(string userId);

        Task<RestaurantDetailsViewModel> GetDetailsByIdAsync(string restaurantId);

        Task<bool> RestaurantExistsById(string restaurantId);

        Task<bool> IsOwnerWithIdOwnedRestaurantWithIdAsync(string restaurantId, string ownerId);

        Task<RestaurantFormModel> GetRestaurantForEditByIdAsync(string restaurantId);

        Task EditRestaurantById(string restaurantId, RestaurantFormModel model);

    }
}
