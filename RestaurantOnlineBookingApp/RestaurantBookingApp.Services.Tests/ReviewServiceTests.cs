using Microsoft.EntityFrameworkCore;
using Moq;
using RestaurantOnlineBooking.Services.Data;
using RestaurantOnlineBookingApp.Data;
using RestaurantOnlineBookingApp.Data.Models;
using RestaurantOnlineBookingApp.Web.ViewModels.Review;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantBookingApp.Services.Tests
{
    public class ReviewServiceTests
    {

        [Test]
        public async Task AddReviewAsyncReviewAddedSuccessfully()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<RestaurantBookingDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using (var dbContext = new RestaurantBookingDbContext(options))
            {
                var reviewService = new ReviewService(dbContext);
                var model = new AddReviewViewModel
                {
                    Rating = 5,
                    Comment = "Good food!",
                    GuestId = Guid.NewGuid(),
                    RestaurantId = Guid.NewGuid()
                };

                // Act
                await reviewService.AddReviewAsync(model);

                // Assert
                var addedReview = dbContext.Reviews.FirstOrDefault();
                Assert.IsNotNull(addedReview);
                Assert.AreEqual(model.Rating, addedReview.ReviewRating);
                Assert.AreEqual(model.Comment, addedReview.Comment);
                Assert.AreEqual(model.GuestId, addedReview.GuestId);
                Assert.AreEqual(model.RestaurantId, addedReview.RestaurantId);
                Assert.IsTrue(DateTime.UtcNow - addedReview.CreatedAt < TimeSpan.FromSeconds(1)); // Check if CreatedAt is within 1 second
            }
        }

        [Test]
        public async Task AddReviewAsyncInvalidModelReturnsException()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<RestaurantBookingDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using (var dbContext = new RestaurantBookingDbContext(options))
            {
                var reviewService = new ReviewService(dbContext);
                var invalidModel = new AddReviewViewModel
                {
                    // Rating is missing
                    Comment = "Invalid model",
                    GuestId = Guid.NewGuid(),
                    RestaurantId = Guid.NewGuid()
                };

                // Act & Assert
                if (!IsValidModel(invalidModel))
                {
                    Assert.ThrowsAsync<InvalidOperationException>(async () =>
                    {
                        await reviewService.AddReviewAsync(invalidModel);
                    });
                }
            }
        }


        [Test]
        public async Task GetReviewsByUserIdAsyncReturnsCorrectReviews()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<RestaurantBookingDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using (var dbContext = new RestaurantBookingDbContext(options))
            {
                DbSeeder.SeedDatabase(dbContext); // Сийдване на базата данни

                var reviewService = new ReviewService(dbContext);
                var userId = DbSeeder.GuestUser.Id; // Взимане на id на потребителя

                // Act
                var reviews = await reviewService.GetReviewsByUserIdAsync(userId.ToString());

                // Assert
                Assert.AreEqual(1, reviews.Count()); // Проверка за броя на ревютата
                var review = reviews.FirstOrDefault();
                Assert.IsNotNull(review); // Проверка за съществуване на ревю
                Assert.AreEqual(DbSeeder.Review.ReviewRating, review.ReviewRating); 
                Assert.AreEqual(DbSeeder.Review.Comment, review.Comment); 
                Assert.AreEqual(DbSeeder.Review.RestaurantId, review.RestaurantId); 
                Assert.AreEqual(DbSeeder.Restaurant.Name, review.RestaurantName); 
            }
        }

        [Test]
        public async Task GetReviewsByUserIdAsyncReturnsEmptyListWhenNoReviewsExist()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<RestaurantBookingDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using (var dbContext = new RestaurantBookingDbContext(options))
            {
                DbSeeder.SeedDatabase(dbContext); 

                var reviewService = new ReviewService(dbContext);
                var userIdWithoutReviews = DbSeeder.OwnerUser1.Id; // Избираме потребител без ревюта

                // Act
                var reviews = await reviewService.GetReviewsByUserIdAsync(userIdWithoutReviews.ToString());

                // Assert
                Assert.IsNotNull(reviews); // Проверка за съществуване на резултата
                Assert.AreEqual(0, reviews.Count()); // Проверка за броя на върнатите ревюта (трябва да е 0)
            }
        }


        [Test]
        public async Task GetReviewsForRestaurantAsyncReturnsEmptyListWhenNoReviewsExist()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<RestaurantBookingDbContext>()
                .UseInMemoryDatabase(databaseName: "GetReviewsForRestaurantAsync_NoReviews")
                .Options;

            using (var dbContext = new RestaurantBookingDbContext(options))
            {
                DbSeeder.SeedDatabase(dbContext); 

                var reviewService = new ReviewService(dbContext);
                var nonExistentRestaurantId = new Guid("11111111-1111-1111-1111-111111111111"); // Несъществуващ id на ресторант

                // Act
                var reviews = await reviewService.GetReviewsForRestaurantAsync(nonExistentRestaurantId);

                // Assert
                Assert.IsNotNull(reviews); // Проверка за съществуване на резултата
                Assert.AreEqual(0, reviews.Count()); // Проверка за броя на върнатите ревюта (трябва да е 0)
            }
        }

        [Test]
        public async Task GetReviewsForRestaurantAsyncReturnsCorrectReviewsWhenReviewsExist()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<RestaurantBookingDbContext>()
                .UseInMemoryDatabase(databaseName: "GetReviewsForRestaurantAsync_WithReviews")
                .Options;

            using (var dbContext = new RestaurantBookingDbContext(options))
            {
                DbSeeder.SeedDatabase(dbContext); 

                var reviewService = new ReviewService(dbContext);
                var restaurantIdWithReviews = DbSeeder.Restaurant.Id; 

                // Act
                var reviews = await reviewService.GetReviewsForRestaurantAsync(restaurantIdWithReviews);

                // Assert
                Assert.IsNotNull(reviews); // Проверка за съществуване на резултата
                Assert.Greater(reviews.Count(), 0); // Проверка дали има върнати ревюта
            }
        }

        [Test]
        public async Task GetSortedReviewsAsyncReturnsSortedByRatingAscending()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<RestaurantBookingDbContext>()
                .UseInMemoryDatabase(databaseName: "GetSortedReviewsAsync_RatingAscending")
                .Options;

            using (var dbContext = new RestaurantBookingDbContext(options))
            {
                DbSeeder.SeedDatabase(dbContext); 

                var reviewService = new ReviewService(dbContext);
                var restaurantIdWithReviews = DbSeeder.Restaurant.Id; 

                // Act
                var sortedReviews = await reviewService.GetSortedReviewsAsync(restaurantIdWithReviews, SortOption.RatingAscending);

                // Assert
                var sortedByRatingAscending = sortedReviews.OrderBy(r => r.ReviewRating);
                Assert.IsTrue(sortedByRatingAscending.SequenceEqual(sortedReviews)); // Проверка дали резултатът е сортиран по оценка възходящо
            }
        }

        [Test]
        public async Task GetSortedReviewsAsyncReturnsSortedByDateNewest()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<RestaurantBookingDbContext>()
                .UseInMemoryDatabase(databaseName: "GetSortedReviewsAsync_DateNewest")
                .Options;

            using (var dbContext = new RestaurantBookingDbContext(options))
            {
                DbSeeder.SeedDatabase(dbContext); 

                var reviewService = new ReviewService(dbContext);
                var restaurantIdWithReviews = DbSeeder.Restaurant.Id; 

                // Act
                var sortedReviews = await reviewService.GetSortedReviewsAsync(restaurantIdWithReviews, SortOption.DateNewest);

                // Assert
                var sortedByDateNewest = sortedReviews.OrderByDescending(r => r.CreatedAt);
                Assert.IsTrue(sortedByDateNewest.SequenceEqual(sortedReviews)); // Проверка дали резултатът е сортиран по дата (първо новите)
            }
        }

        // Helper method to check model validity
        private bool IsValidModel(AddReviewViewModel model)
        {
            return model.Rating > 0; // For example, require Rating to be positive
        }

    }
}
