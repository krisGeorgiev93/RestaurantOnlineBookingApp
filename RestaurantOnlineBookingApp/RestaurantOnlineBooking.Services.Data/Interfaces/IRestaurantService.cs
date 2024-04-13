using RestaurantOnlineBooking.Services.Data.Models;
using RestaurantOnlineBookingApp.Data.Models;
using RestaurantOnlineBookingApp.Web.ViewModels.Home;
using RestaurantOnlineBookingApp.Web.ViewModels.Restaurant;

namespace RestaurantOnlineBooking.Services.Data.Interfaces
{
    public interface IRestaurantService
    {
        Task<Restaurant> GetRestaurantByIdAsync(string restaurantId);
        Task<IEnumerable<AllRestaurantsViewModel>> GetAllAsync();

        Task<string> CreateAndReturnRestaurantIdAsync(RestaurantFormModel model, string ownerId);

        Task<AllRestaurantsFilteredServiceModel> AllAsync(AllRestaurantsQueryModel model);

        Task<IEnumerable<RestaurantAllViewModel>> AllByOwnerIdAsync(string ownerId);

        Task<IEnumerable<RestaurantAllViewModel>> AllByUserIdAsync(string userId);

        Task<RestaurantDetailsViewModel> GetDetailsByIdAsync(string restaurantId);

        Task<bool> RestaurantExistsByIdAsync(string restaurantId);

        Task<bool> IsOwnerWithIdOwnedRestaurantWithIdAsync(string restaurantId, string ownerId);

        Task<RestaurantDeleteDetailsViewModel> GetRestaurantForDeleteByIdAsync(string restaurantId);

        Task DeleteRestaurantByIdAsync(string restaurantId);

        Task<RestaurantFormModel> GetRestaurantForEditByIdAsync(string restaurantId);

        Task EditRestaurantByIdAsync(string restaurantId, RestaurantEditFormModel model);

        Task<(TimeSpan startTime, TimeSpan endTime)> GetRestaurantOperatingHoursAsync(Guid restaurantId);
        List<string> GenerateTimeSlots(TimeSpan startTime, TimeSpan endTime);

        Task AddRestaurantToFavoriteAsync(string userId, Guid restaurantId);

        Task<IEnumerable<Restaurant>> GetFavoriteRestaurantsAsync(string userId);

        Task<bool> IsRestaurantInFavoritesAsync(string userId, string restaurantId);

        Task AddPhotoToRestaurantAsync(string restaurantId, Photo photo);

        Task<IEnumerable<Photo>> GetRestaurantPhotosAsync(string restaurantId);

        Task DeletePhotoAsync(string photoId);

    }
}
