using Microsoft.EntityFrameworkCore;
using Moq;
using RestaurantOnlineBooking.Services.Data;
using RestaurantOnlineBooking.Services.Data.Interfaces;
using RestaurantOnlineBookingApp.Data;
using RestaurantOnlineBookingApp.Data.Models;

namespace RestaurantBookingApp.Services.Tests
{
    public class EventServiceTests
    {
        [Test]
        public async Task EventExistsByIdAsync_EventExists_ReturnsTrue()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<RestaurantBookingDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using (var dbContext = new RestaurantBookingDbContext(options))
            {
                // Seed the database
                DbSeeder.SeedDatabase(dbContext);
                var ownerServiceMock = new Mock<IOwnerService>();
                var photoServiceMock = new Mock<IPhotoService>();

                var eventService = new EventService(dbContext, ownerServiceMock.Object, photoServiceMock.Object);
                var existingEventId = DbSeeder.Event.Id.ToString();

                // Act
                var eventExists = await eventService.EventExistsByIdAsync(existingEventId);

                // Assert
                Assert.IsTrue(eventExists);
            }
        }

        [Test]
        public async Task EventExistsByIdAsync_EventDoesNotExist_ReturnsFalse()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<RestaurantBookingDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using (var dbContext = new RestaurantBookingDbContext(options))
            {
                // Seed the database
                DbSeeder.SeedDatabase(dbContext);
                var ownerServiceMock = new Mock<IOwnerService>();
                var photoServiceMock = new Mock<IPhotoService>();
                var eventService = new EventService(dbContext, ownerServiceMock.Object, photoServiceMock.Object);
                var nonExistingEventId = Guid.NewGuid().ToString();

                // Act
                var eventExists = await eventService.EventExistsByIdAsync(nonExistingEventId);

                // Assert
                Assert.IsFalse(eventExists);
            }
        }

        [Test]
        public async Task GetEventByIdAsync_ReturnsCorrectEvent()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<RestaurantBookingDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using (var dbContext = new RestaurantBookingDbContext(options))
            {
                var eventId = Guid.NewGuid().ToString();
                var expectedEvent = new Event
                {
                    Id = Guid.Parse(eventId),
                    Title = "Test Event",
                    Description = "test event",
                    Date = DateTime.Today,
                    Time = new TimeSpan(19, 0, 0),
                    Price = 30.00m,
                    RestaurantId = Guid.NewGuid(),
                    ImageUrl = "TestImageUrl"
                };
                await dbContext.Events.AddAsync(expectedEvent);
                await dbContext.SaveChangesAsync();
                var ownerServiceMock = new Mock<IOwnerService>();
                var photoServiceMock = new Mock<IPhotoService>();
                var eventService = new EventService(dbContext, ownerServiceMock.Object, photoServiceMock.Object);

                // Act
                var actualEventForm = await eventService.GetEventByIdAsync(eventId);

                // Assert
                Assert.IsNotNull(actualEventForm);
                Assert.AreEqual(expectedEvent.Title, actualEventForm.Title);
                Assert.AreEqual(expectedEvent.Description, actualEventForm.Description);
             
            }
        }


    }
}
