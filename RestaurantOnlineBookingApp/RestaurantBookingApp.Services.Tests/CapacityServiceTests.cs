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
    public class CapacityServiceTests
    {

        private RestaurantBookingDbContext _dbContext;
        private CapacityService capacityService;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<RestaurantBookingDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _dbContext = new RestaurantBookingDbContext(options);
            capacityService = new CapacityService(this._dbContext);
        }

        [TearDown]
        public void TearDown()
        {
            _dbContext.Database.EnsureDeleted();
        }
        [Test]
        public async Task AddCapacitiesFor60DaysAsync_WhenValidInput_AddsCapacitiesFor60Days()
        {
            // Arrange
            var restaurantId = Guid.NewGuid();
            var capacity = 50;
            var startDate = DateTime.Now.Date;
            var startDateString = startDate.ToString("MM-dd-yyyy");

            // Act
            await capacityService.AddCapacitiesFor60DaysAsync(restaurantId, capacity, startDateString);

            // Assert
            for (int i = 0; i < 60; i++)
            {
                var currentDate = startDate.AddDays(i);
                var existingCapacity = await _dbContext.CapacitiesParDate
                    .FirstOrDefaultAsync(c => c.RestaurantId == restaurantId && c.Date == currentDate);

                Assert.IsNotNull(existingCapacity);
                Assert.AreEqual(capacity, existingCapacity.Capacity);
            }
        }
    }
}
