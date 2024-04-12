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
        public async Task GetAllEventsByRestaurantIdAsyncShouldReturnAllEventsForGivenRestaurantId()
        {
            // Arrange
            var ownerServiceMock = new Mock<IOwnerService>();
            var photoServiceMock = new Mock<IPhotoService>();

            using (var dbContext = new RestaurantBookingDbContext(this.dbContextOptions))
            {
                var eventService = new EventService(dbContext, ownerServiceMock.Object, photoServiceMock.Object);

                // Act
                var restaurantId = DbSeeder.Restaurant.Id;
                var result = await eventService.GetAllEventsByRestaurantIdAsync(restaurantId.ToString());

                // Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(1, result.Count());
                Assert.AreEqual("Test Event", result.First().Title);
            }
        }

        [Test]
        public async Task GetAllEventsByRestaurantIdAsyncShouldReturnEmptyListIfNoEventsExist()
        {
            // Arrange
            var ownerServiceMock = new Mock<IOwnerService>();
            var photoServiceMock = new Mock<IPhotoService>();
            using (var dbContext = new RestaurantBookingDbContext(this.dbContextOptions))
            {
                var eventService = new EventService(dbContext, ownerServiceMock.Object, photoServiceMock.Object);

                DbSeeder.SeedDatabase(dbContext);

                var nonExistingRestaurantId = Guid.NewGuid().ToString();

                // Act
                var events = await eventService.GetAllEventsByRestaurantIdAsync(nonExistingRestaurantId);

                // Assert
                Assert.IsNotNull(events);
                Assert.AreEqual(0, events.Count());
            }
        }
        [Test]
        public async Task EventExistsByIdAsyncEventExistsReturnsTrue()
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
        public async Task EventExistsByIdAsyncEventDoesNotExistReturnsFalse()
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
        public async Task GetEventByIdAsyncReturnsCorrectEvent()
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
