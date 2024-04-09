using Microsoft.EntityFrameworkCore;
using RestaurantOnlineBooking.Services.Data;
using RestaurantOnlineBookingApp.Data;
using RestaurantOnlineBookingApp.Data.Models;

namespace RestaurantBookingApp.Services.Tests
{
    public class BookingServiceTests
    {
        private BookingService _bookingService;
        private RestaurantBookingDbContext _dbContext;

        [SetUp]
        public void Setup()
        {
            // Подготовка на Mock DbContext
            var options = new DbContextOptionsBuilder<RestaurantBookingDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _dbContext = new RestaurantBookingDbContext(options);

            // Подготовка на BookingService с Mock DbContext
            _bookingService = new BookingService(this._dbContext);
        }

        [TearDown]
        public void TearDown()
        {
            // Изчистване на данните във временната паметна база
            this._dbContext.Database.EnsureDeleted();
        }

        [Test]
        public async Task BookingExistsByIdAsyncWhenBookingExistsShouldReturnTrue()
        {
            // Arrange
            var bookingId = Guid.NewGuid();
            var booking = new Booking
            {
                Id = bookingId,
                Email = "test@example.com",
                FirstName = "John",
                LastName = "Doe",
                PhoneNumber = "1234567890"
            };
            await _dbContext.Bookings.AddAsync(booking);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _bookingService.BookingExistsByIdAsync(bookingId.ToString());

            // Assert
            Assert.IsTrue(result);
        }
        [Test]
        public async Task BookingExistsByIdAsyncWhenBookingDoesNotExistShouldReturnFalse()
        {
            // Arrange
            var bookingId = Guid.NewGuid();

            // Act
            var result = await _bookingService.BookingExistsByIdAsync(bookingId.ToString());

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public async Task GetBookingByIdAsyncWhenValidBookingIdShouldReturnBookingViewModel()
        {
            // Arrange
            var bookingId = Guid.NewGuid().ToString();
            var booking = new Booking
            {
                Id = Guid.Parse(bookingId),
                FirstName = "Kris",
                LastName = "Georgiev",
                Email = "kris@mail.com",
                PhoneNumber = "1234567890",
                BookingDate = DateTime.Today.AddDays(1),
                ReservedTime = new TimeSpan(13, 30, 0),
                NumberOfGuests = 5
            };
            await _dbContext.Bookings.AddAsync(booking);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _bookingService.GetBookingByIdAsync(bookingId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Kris", result.FirstName);
            Assert.AreEqual("Georgiev", result.LastName);
            Assert.AreEqual("kris@mail.com", result.Email);
            Assert.AreEqual("1234567890", result.PhoneNumber);
            Assert.AreEqual("13:30:00", result.ReservedTime); // Проверяваме дали времето е в правилния формат
            Assert.AreEqual(DateTime.Today.AddDays(1).ToString(), result.BookingDate); // Проверяваме дали датата е в правилния формат
            Assert.AreEqual(5, result.NumberOfGuests);
        }

        [Test]
        public async Task GetBookingByIdAsyncWhenInvalidBookingIdShouldThrowException()
        {
            // Arrange
            var invalidBookingId = Guid.NewGuid().ToString();

            // Act & Assert
            var ex = Assert.ThrowsAsync<InvalidOperationException>(async () => await _bookingService.GetBookingByIdAsync(invalidBookingId));
            Assert.That(ex.Message, Is.EqualTo("Sequence contains no elements"));
        }

        [Test]
        public async Task GetBookingsByRestaurantIdAsyncWhenValidRestaurantIdShouldReturnBookings()
        {
            // Arrange
            var restaurantId = Guid.NewGuid().ToString();
            var restaurant = new Restaurant
            {
                Id = Guid.Parse(restaurantId),
                Name = "Test",
                Address = "Test",
                Description = "Test",
                ImageUrl = "test_image",
                StartingTime = new TimeSpan(9, 0, 0),
                EndingTime = new TimeSpan(22, 0, 0),
                Capacity = 50,
                CityId = 1,
                CategoryId = 1,
                OwnerId = Guid.NewGuid()
            };
            await _dbContext.Restaurants.AddAsync(restaurant);

            var bookings = new List<Booking>
            {
                new Booking
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Kris",
                    LastName = "Georgiev",
                    Email = "kris@mail.com",
                    PhoneNumber = "1234567890",
                    BookingDate = DateTime.Today.AddDays(1),
                    ReservedTime = new TimeSpan(12, 0, 0),
                    NumberOfGuests = 4,
                    RestaurantId = restaurant.Id
                }
            };
            await _dbContext.Bookings.AddRangeAsync(bookings);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _bookingService.GetBookingsByRestaurantIdAsync(restaurantId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count()); // Проверяваме дали връщаният списък съдържа една резервация

        }

        [Test]
        public void GetBookingsByRestaurantIdAsyncWhenInvalidRestaurantIdShouldThrowException()
        {
            // Arrange
            var invalidRestaurantId = "invalid_id";

            // Act & Assert
            var ex = Assert.ThrowsAsync<ArgumentException>(async () => await _bookingService.GetBookingsByRestaurantIdAsync(invalidRestaurantId));
            Assert.That(ex.Message, Is.EqualTo("Invalid restaurantId"));
        }


        [Test]
        public async Task GetBookingsByUserIdAsync_WhenValidUserId_ReturnsBookings()
        {
            // Arrange
            var userId = Guid.NewGuid().ToString();

            // Добавяне на тестови данни в базата данни
            var restaurantId = Guid.NewGuid();
            var restaurant = new Restaurant
            {
                Id = restaurantId,
                Name = "Test",
                Address = "Test",
                Description = "Test",
                ImageUrl = "test_image",
                StartingTime = new TimeSpan(9, 0, 0),
                EndingTime = new TimeSpan(22, 0, 0),
                Capacity = 50,
                CityId = 1,
                CategoryId = 1,
                OwnerId = Guid.NewGuid()
            };
            await _dbContext.Restaurants.AddAsync(restaurant);

            var bookings = new List<Booking>
        {
            new Booking
            {
                Id = Guid.NewGuid(),
                BookingDate = DateTime.Now.Date,
                ReservedTime = new TimeSpan(12, 0, 0),
                NumberOfGuests = 4,
                RestaurantId = restaurantId,
                GuestId = Guid.Parse(userId),
                FirstName = "Kris",
                LastName = "Georgiev",
                Email = "kris@mail.com",
                PhoneNumber = "123456789"
            },
            new Booking
            {
                Id = Guid.NewGuid(),
                BookingDate = DateTime.Now.Date.AddDays(1),
                ReservedTime = new TimeSpan(18, 0, 0),
                NumberOfGuests = 2,
                RestaurantId = restaurantId,
                GuestId = Guid.Parse(userId),
                FirstName = "Kris",
                LastName = "Georgiev",
                Email = "kris@mail.com",
                PhoneNumber = "123456789"
            }
        };
            _dbContext.Bookings.AddRange(bookings);
            _dbContext.SaveChanges();

            // Act
            var result = await _bookingService.GetBookingsByUserIdAsync(userId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());

            var firstBooking = result.First();
            Assert.IsNotNull(firstBooking);
            Assert.AreEqual(bookings[0].Id, firstBooking.Id);
        }

        [Test]
        public async Task GetBookingsByUserIdAsyncWhenInvalidUserIdReturnsEmptyList()
        {
            // Arrange
            var invalidUserId = "invalid_user";

            // Act
            var result = await _bookingService.GetBookingsByUserIdAsync(invalidUserId);

            // Assert
            Assert.IsEmpty(result);
        }

        [Test]
        public void GetReservedTimesReturnsCorrectList()
        {
            // Arrange
            var startingTime = new TimeSpan(9, 0, 0); // 9:00 AM
            var endingTime = new TimeSpan(17, 0, 0);  // 5:00 PM
            var interval = new TimeSpan(1, 0, 0);     // 1 hour interval

            var expectedTimes = new List<string>
    {
        "09:00", "10:00", "11:00", "12:00", "13:00", "14:00", "15:00", "16:00", "17:00"
    };

            // Act
            var result = _bookingService.GetReservedTimes(startingTime, endingTime, interval);

            // Assert
            CollectionAssert.AreEqual(expectedTimes, result);
        }

        [Test]
        public void GetReservedTimesWhenStartingTimeAfterEndingTimeReturnsEmptyList()
        {
            // Arrange
            var startingTime = new TimeSpan(18, 0, 0); // 6:00 PM
            var endingTime = new TimeSpan(17, 0, 0);   // 5:00 PM
            var interval = new TimeSpan(1, 0, 0);      // 1 hour interval

            // Act
            var result = _bookingService.GetReservedTimes(startingTime, endingTime, interval);

            // Assert
            Assert.IsEmpty(result);
        }
    }
}
