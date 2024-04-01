namespace RestaurantOnlineBooking.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using RestaurantOnlineBooking.Services.Data.Interfaces;
    using RestaurantOnlineBookingApp.Data;
    using RestaurantOnlineBookingApp.Data.Models;
    using RestaurantOnlineBookingApp.Web.ViewModels.Review;
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
                GuestId = model.GuestId,
                RestaurantId = model.RestaurantId,
                CreatedAt = DateTime.Now
            };

            await this._dbContext.Reviews.AddAsync(review);
            await this._dbContext.SaveChangesAsync();
        }       

        public async Task<IEnumerable<ReviewDetailsViewModel>> GetReviewsByUserIdAsync(string userId)
        {
            return await this._dbContext.Reviews
               .Where(r => r.GuestId.ToString() == userId)
               .Select(r => new ReviewDetailsViewModel
               {
                   ReviewRating = r.ReviewRating,
                   Comment = r.Comment,
                   RestaurantId = r.RestaurantId,
                   RestaurantName = r.Restaurant.Name                   
               })
               .ToListAsync();
        }

        public async Task<IEnumerable<ReviewAllViewModel>> GetReviewsForRestaurantAsync(Guid restaurantId)
        {
            var reviews = _dbContext.Reviews
                 .Where(r => r.RestaurantId == restaurantId)
                 .Select(r => new ReviewAllViewModel
                 {
                     Id = r.Id,
                     ReviewRating = r.ReviewRating,
                     Comment = r.Comment,
                     GuestId = r.GuestId,
                     RestaurantId = r.RestaurantId,
                     GuestEmail = r.Guest.Email,
                     CreatedAt = r.CreatedAt
                 });

            return await reviews.ToListAsync();
        }
    }
}
