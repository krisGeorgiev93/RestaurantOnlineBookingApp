using Microsoft.EntityFrameworkCore;
using RestaurantOnlineBooking.Services.Data;
using RestaurantOnlineBookingApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantBookingApp.Services.Tests
{
    public class CityServiceTests
    {
        private DbContextOptions<RestaurantBookingDbContext> dbContextOptions;

        [SetUp]
        public void Setup()
        {
            this.dbContextOptions = new DbContextOptionsBuilder<RestaurantBookingDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using (var dbContext = new RestaurantBookingDbContext(this.dbContextOptions))
            {
                DbSeeder.SeedDatabase(dbContext);
            }
        }

        [Test]
        public async Task AllCitiesNamesAsyncShouldReturnAllCitiesNames()
        {
            // Arrange
            using (var dbContext = new RestaurantBookingDbContext(this.dbContextOptions))
            {
                var cityService = new CityService(dbContext);

                // Act
                var result = await cityService.AllCitiesNamesAsync();

                // Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(2, result.Count()); 

                Assert.IsTrue(result.Contains("Sofia"));

                Assert.IsTrue(result.Contains("Plovdiv"));
            }
        }

        [Test]
        public async Task AllCitiesNamesAsyncShouldReturnEmptyListIfNoCities()
        {
            // Arrange
            using (var dbContext = new RestaurantBookingDbContext(this.dbContextOptions))
            {
                // Изтриваме всички градове от базата данни
                dbContext.Cities.RemoveRange(dbContext.Cities);
                await dbContext.SaveChangesAsync();

                var cityService = new CityService(dbContext);

                // Act
                var result = await cityService.AllCitiesNamesAsync();

                // Assert
                Assert.IsNotNull(result);
                Assert.IsEmpty(result); // Очакваме да получим празен списък, тъй като няма градове в базата данни
            }
        }

        [Test]
        public async Task GetAllCitiesAsyncShouldReturnAllCities()
        {
            // Arrange
            using (var dbContext = new RestaurantBookingDbContext(this.dbContextOptions))
            {
                var cityService = new CityService(dbContext);

                // Act
                var result = await cityService.GetAllCitiesAsync();

                // Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(2, result.Count()); // Очакваме да има 2 града в базата данни

                // Проверка на името на първия град
                var firstCity = result.FirstOrDefault();
                Assert.IsNotNull(firstCity);
                Assert.AreEqual("Sofia", firstCity.Name);

                // Проверка на името на втория град
                var secondCity = result.Skip(1).FirstOrDefault();
                Assert.IsNotNull(secondCity);
                Assert.AreEqual("Plovdiv", secondCity.Name);
            }
        }

        [Test]
        public async Task GetAllCitiesAsyncShouldReturnEmptyListIfNoCities()
        {
            // Arrange
            using (var dbContext = new RestaurantBookingDbContext(this.dbContextOptions))
            {
                // Изтриваме всички градове от базата данни
                dbContext.Cities.RemoveRange(dbContext.Cities);
                await dbContext.SaveChangesAsync();

                var cityService = new CityService(dbContext);

                // Act
                var result = await cityService.GetAllCitiesAsync();

                // Assert
                Assert.IsNotNull(result);
                Assert.IsEmpty(result); // Очакваме да получим празен списък, тъй като няма градове в базата данни
            }
        }
    }
}
