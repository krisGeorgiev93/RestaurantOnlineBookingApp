using Microsoft.EntityFrameworkCore;
using Moq;
using RestaurantOnlineBooking.Services.Data;
using RestaurantOnlineBooking.Services.Data.Interfaces;
using RestaurantOnlineBookingApp.Data;
using RestaurantOnlineBookingApp.Data.Models;

namespace RestaurantBookingApp.Services.Tests
{
    public class MealServiceTests
    {
        private DbContextOptions<RestaurantBookingDbContext> dbOptions;
        private RestaurantBookingDbContext dbContext;
        private MealService mealService;

        [SetUp]
        public void Setup()
        {
            this.dbOptions = new DbContextOptionsBuilder<RestaurantBookingDbContext>()
                .UseInMemoryDatabase("TestDatabase")
                .Options;
            this.dbContext = new RestaurantBookingDbContext(this.dbOptions);
            this.mealService = new MealService(this.dbContext, new Mock<IPhotoService>().Object);
        }


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

        [Test]
        public async Task GetAllMealsAsyncReturnsAllMeals()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<RestaurantBookingDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using (var dbContext = new RestaurantBookingDbContext(options))
            {
                var expectedMeals = new List<Meal>
        {
            new Meal { Id = 1, Name = "Meal 1", Description = "Description 1", Price = 10.00m, ImageUrl = "image1.jpg" },
            new Meal { Id = 2, Name = "Meal 2", Description = "Description 2", Price = 15.00m, ImageUrl = "image2.jpg" },
            new Meal { Id = 3, Name = "Meal 3", Description = "Description 3", Price = 20.00m, ImageUrl = "image3.jpg" }
        };

                dbContext.Meals.AddRange(expectedMeals);
                await dbContext.SaveChangesAsync();

                var mealService = new MealService(dbContext, Mock.Of<IPhotoService>());

                // Act
                var actualMeals = await mealService.GetAllMealsAsync();

                // Assert
                Assert.AreEqual(expectedMeals.Count, actualMeals.Count());

                foreach (var expectedMeal in expectedMeals)
                {
                    var actualMeal = actualMeals.FirstOrDefault(m => m.Id == expectedMeal.Id.ToString());
                    Assert.IsNotNull(actualMeal);
                    Assert.AreEqual(expectedMeal.Name, actualMeal.Name);
                    Assert.AreEqual(expectedMeal.Description, actualMeal.Description);
                    Assert.AreEqual(expectedMeal.Price, actualMeal.Price);
                    Assert.AreEqual(expectedMeal.ImageUrl, actualMeal.ImageUrl);
                }
            }
        }

        [Test]
        public async Task GetAllMealsAsyncReturnsEmptyListWhenNoMealsExist()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<RestaurantBookingDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using (var dbContext = new RestaurantBookingDbContext(options))
            {
                var mealService = new MealService(dbContext, Mock.Of<IPhotoService>());

                // Act
                var actualMeals = await mealService.GetAllMealsAsync();

                // Assert
                Assert.IsNotNull(actualMeals);
                Assert.AreEqual(0, actualMeals.Count());
            }
        }

        [Test]
        public async Task GetAllMealsForRestaurantByIdAsyncReturnsAllMealsForRestaurant()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<RestaurantBookingDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using (var dbContext = new RestaurantBookingDbContext(options))
            {
                var restaurantId = Guid.NewGuid().ToString();
                var expectedMeals = new List<Meal>
        {
            new Meal { Id = 1, Name = "Meal 1", Description = "Description 1", Price = 10.00m, ImageUrl = "image1.jpg", RestaurantId = Guid.Parse(restaurantId) },
            new Meal { Id = 2, Name = "Meal 2", Description = "Description 2", Price = 15.00m, ImageUrl = "image2.jpg", RestaurantId = Guid.Parse(restaurantId) },
        };

                dbContext.Meals.AddRange(expectedMeals);
                await dbContext.SaveChangesAsync();

                var mealService = new MealService(dbContext, Mock.Of<IPhotoService>());

                // Act
                var actualMeals = await mealService.GetAllMealsForRestaurantByIdAsync(restaurantId);

                // Assert
                Assert.AreEqual(expectedMeals.Count(m => m.RestaurantId == Guid.Parse(restaurantId)), actualMeals.Count());

                foreach (var expectedMeal in expectedMeals.Where(m => m.RestaurantId == Guid.Parse(restaurantId)))
                {
                    var actualMeal = actualMeals.FirstOrDefault(m => m.Id == expectedMeal.Id);
                    Assert.IsNotNull(actualMeal);
                    Assert.AreEqual(expectedMeal.Name, actualMeal.Name);
                    Assert.AreEqual(expectedMeal.Description, actualMeal.Description);
                    Assert.AreEqual(expectedMeal.Price, actualMeal.Price);
                    Assert.AreEqual(expectedMeal.ImageUrl, actualMeal.ImageUrl);
                }
            }
        }

        [Test]
        public async Task GetAllMealsForRestaurantByIdAsync_ReturnsEmptyList_WhenNoMealsExistForRestaurant()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<RestaurantBookingDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using (var dbContext = new RestaurantBookingDbContext(options))
            {
                var restaurantId = Guid.NewGuid().ToString();
                var mealService = new MealService(dbContext, Mock.Of<IPhotoService>());

                // Act
                var actualMeals = await mealService.GetAllMealsForRestaurantByIdAsync(restaurantId);

                // Assert
                Assert.IsNotNull(actualMeals);
                Assert.AreEqual(0, actualMeals.Count());
            }
        }

        [Test]
        public async Task GetMealByIdAsyncReturnsCorrectMeal()
        {
            // Arrange
            var mealId = 1;
            var expectedMeal = new Meal
            {
                Id = mealId,
                Name = "Test Meal",
                Description = "Test description",
                Price = 20.00m,
                ImageUrl = "test-image-url",
                RestaurantId = Guid.NewGuid()
            };

            var options = new DbContextOptionsBuilder<RestaurantBookingDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using (var dbContext = new RestaurantBookingDbContext(options))
            {
                await dbContext.Meals.AddAsync(expectedMeal);
                await dbContext.SaveChangesAsync();

                var photoServiceMock = new Mock<IPhotoService>();
                var mealService = new MealService(dbContext, photoServiceMock.Object);

                // Act
                var actualMealForm = await mealService.GetMealByIdAsync(mealId.ToString());

                // Assert
                Assert.IsNotNull(actualMealForm);
                Assert.AreEqual(expectedMeal.Id, actualMealForm.Id);
                Assert.AreEqual(expectedMeal.Name, actualMealForm.Name);
                Assert.AreEqual(expectedMeal.Description, actualMealForm.Description);
                Assert.AreEqual(expectedMeal.Price, actualMealForm.Price);
                Assert.AreEqual(expectedMeal.RestaurantId, actualMealForm.RestaurantId);
            }
        }

        [Test]
        public async Task GetMealByIdAsyncReturnsErrorWhenMealNotFound()
        {
            // Arrange
            var mealId = 999; 

            var options = new DbContextOptionsBuilder<RestaurantBookingDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using (var dbContext = new RestaurantBookingDbContext(options))
            {
                var photoServiceMock = new Mock<IPhotoService>();
                var mealService = new MealService(dbContext, photoServiceMock.Object);

                // Act & Assert
                // Проверяваме дали методът хвърля изключение при опит за намиране на несъществуващо ястие
                Assert.ThrowsAsync<InvalidOperationException>(() => mealService.GetMealByIdAsync(mealId.ToString()));
            }
        }

        [Test]
        public async Task MealExistsByIdAsyncReturnsTrueWhenMealExists()
        {
            // Arrange
            var mealId = 1;
            var meal = new Meal
            {
                Id = mealId,
                Name = "Test Meal",
                Description = "Test description",
                Price = 20.00m,
                ImageUrl = "test-image-url",
                RestaurantId = Guid.NewGuid()
            };

            await dbContext.Meals.AddAsync(meal);
            await dbContext.SaveChangesAsync();

            // Act
            var result = await mealService.MealExistsByIdAsync(mealId.ToString());

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task MealExistsByIdAsyncReturnsFalseWhenMealDoesNotExist()
        {
            // Arrange
            var mealId = "999"; // meal with ID 999 doesn't exist

            // Act
            var mealExists = await this.mealService.MealExistsByIdAsync(mealId);

            // Assert
            Assert.IsFalse(mealExists);
        }
    }
}


