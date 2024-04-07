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
        public async Task AddReviewAsync_ReviewAddedSuccessfully()
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
        public async Task AddReviewAsync_InvalidModel_ReturnsException()
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

        // Helper method to check model validity
        private bool IsValidModel(AddReviewViewModel model)
        {
            // Add any validation logic here
            return model.Rating > 0; // For example, require Rating to be positive
        }

    }
}
