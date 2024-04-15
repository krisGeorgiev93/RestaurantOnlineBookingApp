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
                CreatedAt = DateTime.UtcNow
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
                  .OrderByDescending(r => r.ReviewRating) // default sorting
                 .Select(r => new ReviewAllViewModel
                 {
                     Id = r.Id,
                     ReviewRating = r.ReviewRating,
                     Comment = r.Comment,
                     GuestId = r.GuestId,
                     RestaurantId = r.RestaurantId,
                     GuestEmail = r.Guest.Email,
                     FirstName = r.Guest.FirstName,
                     LastName = r.Guest.LastName,
                     CreatedAt = r.CreatedAt
                 });

            return await reviews.ToListAsync();
        }

        public async Task<IEnumerable<ReviewAllViewModel>> GetSortedReviewsAsync(Guid restaurantId, SortOption sortBy)
        {
            var reviews = await this.GetReviewsForRestaurantAsync(restaurantId);

            switch (sortBy)
            {
                case SortOption.RatingAscending:
                    reviews = reviews.OrderBy(r => r.ReviewRating);
                    break;
                case SortOption.RatingDescending:
                    reviews = reviews.OrderByDescending(r => r.ReviewRating);
                    break;
                case SortOption.DateNewest:
                    reviews = reviews.OrderByDescending(r => r.CreatedAt);
                    break;
                case SortOption.DateOldest:
                    reviews = reviews.OrderBy(r => r.CreatedAt);
                    break;
                default:
                    // No sorting
                    break;
            }

            return reviews;
        }


        public async Task<bool> HasReviewed(string restaurantId, string guestId)
        {
            return await _dbContext.Reviews.AnyAsync(r => r.RestaurantId.ToString() == restaurantId && r.GuestId.ToString() == guestId);
        }
       
    }
}
