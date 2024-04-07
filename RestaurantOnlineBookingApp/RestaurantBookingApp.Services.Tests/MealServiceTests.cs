using Microsoft.EntityFrameworkCore;
using Moq;
using RestaurantOnlineBooking.Services.Data.Interfaces;
using RestaurantOnlineBooking.Services.Data;
using RestaurantOnlineBookingApp.Data.Models;
using RestaurantOnlineBookingApp.Data;
using RestaurantOnlineBookingApp.Web.ViewModels.Meal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace RestaurantBookingApp.Services.Tests
{
    public class MealServiceTests
    {
        [Test]
        public async Task DeleteMealAsyncMealExistsRemovesMeal()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<RestaurantBookingDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using (var dbContext = new RestaurantBookingDbContext(options))
            {
                var restaurant = new Restaurant
                {
                    Id = Guid.NewGuid(),
                    Name = "Test",
                    Address = "Test",
                    Description = "Test",
                    ImageUrl = "test.jpg"
                };

                var meal = new Meal
                {
                    Id = 1,
                    Name = "Test",
                    Description = "Test",
                    Price = 10.00m,
                    ImageUrl = "test.jpg",
                    RestaurantId = restaurant.Id
                };

                dbContext.Restaurants.Add(restaurant);
                dbContext.Meals.Add(meal);
                await dbContext.SaveChangesAsync();

                var mealService = new MealService(dbContext, Mock.Of<IPhotoService>());

                // Act
                await mealService.DeleteMealAsync(meal.Id.ToString());

                // Assert
                var deletedMeal = await dbContext.Meals.FirstOrDefaultAsync(m => m.Id == meal.Id);
                Assert.IsNull(deletedMeal);
            }
        }

        [Test]
        public void DeleteMealAsyncIfInvalidMealIdThrowsException()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<RestaurantBookingDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using (var dbContext = new RestaurantBookingDbContext(options))
            {
                var mealService = new MealService(dbContext, Mock.Of<IPhotoService>());

                // Act & Assert
                Assert.ThrowsAsync<InvalidOperationException>(async () => await mealService.DeleteMealAsync("invalid_id"));
            }
        }


    }
}

