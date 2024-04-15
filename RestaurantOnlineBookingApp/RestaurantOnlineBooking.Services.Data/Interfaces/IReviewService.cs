using RestaurantOnlineBookingApp.Web.ViewModels.Review;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantOnlineBooking.Services.Data.Interfaces
{
    public interface IReviewService
    {
        Task AddReviewAsync(AddReviewViewModel model);

        Task<IEnumerable<ReviewDetailsViewModel>> GetReviewsByUserIdAsync(string userId);

        Task<IEnumerable<ReviewAllViewModel>> GetReviewsForRestaurantAsync(Guid restaurantId);

        Task<IEnumerable<ReviewAllViewModel>> GetSortedReviewsAsync(Guid restaurantId, SortOption sortBy);

        Task<bool> HasReviewed(string restaurantId, string guestId);


    }
}
