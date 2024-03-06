using RestaurantOnlineBooking.Services.Data.Interfaces;
using RestaurantOnlineBookingApp.Data;
using RestaurantOnlineBookingApp.Data.Models;
using RestaurantOnlineBookingApp.Web.ViewModels.Review;

namespace RestaurantOnlineBooking.Services.Data
{
    public class ReviewService : IReviewService
    {
        private RestaurantBookingDbContext _dbContext;

        public ReviewService(RestaurantBookingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddReviewAsync(AddReviewViewModel model)
        {
            var review = new Review
            {
                ReviewRating = model.Rating,
                Comment = model.Comment,
                RestaurantId = model.RestaurantId,
                GuestId = model.GuestId,
            };

            await this._dbContext.Reviews.AddAsync(review);
            await this._dbContext.SaveChangesAsync();
        }
    }
}
