using Microsoft.EntityFrameworkCore;
using RestaurantOnlineBooking.Services.Data;
using RestaurantOnlineBookingApp.Data;
using RestaurantOnlineBookingApp.Data.Models;
using RestaurantOnlineBookingApp.Web.ViewModels.Booking;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
