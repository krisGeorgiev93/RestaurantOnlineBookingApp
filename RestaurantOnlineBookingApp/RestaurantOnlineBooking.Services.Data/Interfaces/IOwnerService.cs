using RestaurantOnlineBookingApp.Data.Models;
using RestaurantOnlineBookingApp.Web.ViewModels.Owner;

namespace RestaurantOnlineBooking.Services.Data.Interfaces
{
    public interface IOwnerService
    {
        Task<bool> OwnerExistByIdAsync(string id);

        Task<bool> OwnerExistsByPhoneNumberAsync(string phoneNumber);

        Task<bool> HasRestaurantWithIdAsync(string? userId, string restaurantId);

        Task<string?> OwnerIdByUserIdAsync(string userId);

        Task Create(string userId, JoinOwnerFormModel model);
        Task<List<Restaurant>> GetOwnedRestaurantsAsync(Guid ownerId);
    }
}
