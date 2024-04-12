using Microsoft.EntityFrameworkCore;
using Moq;
using RestaurantOnlineBooking.Services.Data;
using RestaurantOnlineBooking.Services.Data.Interfaces;
using RestaurantOnlineBookingApp.Data;

namespace RestaurantBookingApp.Services.Tests
{
    public class UserServiceTests
    {

        private DbContextOptions<RestaurantBookingDbContext> dbContextOptions;

        [SetUp]
        public void Setup()
        {
            // Инициализация на in-memory database и контекста
            this.dbContextOptions = new DbContextOptionsBuilder<RestaurantBookingDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using (var dbContext = new RestaurantBookingDbContext(this.dbContextOptions))
            {
                // Попълване на тестови данни в базата данни
                DbSeeder.SeedDatabase(dbContext);
            }
        }

        [Test]
        public async Task AllAsyncShouldReturnAllUsersWithOwnerPhoneNumbers()
        {
            // Arrange
            using (var dbContext = new RestaurantBookingDbContext(this.dbContextOptions))
            {
                var userService = new UserService(dbContext);

                // Act
                var result = await userService.AllAsync();

                // Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(3, result.Count()); // Очакваме да имаме 3 потребителя


                // Проверка на данните на първия потребител
                var user1Id = result.First().Id; // Извличаме ID на първия потребител от резултатите
                var user1 = result.FirstOrDefault(u => u.Id == user1Id);
                Assert.IsNotNull(user1);
                Assert.AreEqual("ivan@mail.com", user1.Email);
                Assert.AreEqual("Ivan Ivanov", user1.FullName);
                Assert.AreEqual("+359767672654", user1.PhoneNumber);

                // Проверка на данните на втория потребител
                var user2Id = result.Skip(1).First().Id; 
                var user2 = result.FirstOrDefault(u => u.Id == user2Id);
                Assert.IsNotNull(user2);
                Assert.AreEqual("hristo@mail.com", user2.Email);
                Assert.AreEqual("Hristo Hristov", user2.FullName);
                Assert.AreEqual("+359765625455", user2.PhoneNumber);

                // Проверка на данните на третия потребител
                var user3Id = result.Skip(2).First().Id; 
                var user3 = result.FirstOrDefault(u => u.Id == user3Id);
                Assert.IsNotNull(user3);
                Assert.AreEqual("kiro@mail.com", user3.Email);
                Assert.AreEqual("Kiro Petkov", user3.FullName);
                Assert.IsEmpty(user3.PhoneNumber); // Третият потребител не е собственик, затова PhoneNumber трябва да е празен
            }
        }

        [Test]
        public async Task GetFullNameByEmailAsyncShouldReturnFullNameIfExists()
        {
            // Arrange
            var email = "ivan@mail.com";
            var expectedFullName = "Ivan Ivanov";

            using (var dbContext = new RestaurantBookingDbContext(this.dbContextOptions))
            {
                var userService = new UserService(dbContext);

                // Act
                var result = await userService.GetFullNameByEmailAsync(email);

                // Assert
                Assert.AreEqual(expectedFullName, result);
            }
        }

        [Test]
        public async Task GetFullNameByEmailAsyncShouldReturnEmptyStringIfNotExists()
        {
            // Arrange
            var email = "invalid_email@email.com";
            var expectedFullName = string.Empty;

            using (var dbContext = new RestaurantBookingDbContext(this.dbContextOptions))
            {
                var userService = new UserService(dbContext);

                // Act
                var result = await userService.GetFullNameByEmailAsync(email);

                // Assert
                Assert.AreEqual(expectedFullName, result);
            }
        }

        [Test]
        public async Task GetFullNameByIdAsyncShouldReturnFullNameIfExists()
        {
            // Arrange
            using (var dbContext = new RestaurantBookingDbContext(this.dbContextOptions))
            {              
                var userService = new UserService(dbContext);

                // Вземаме Id на първия потребител
                var userId = DbSeeder.OwnerUser1.Id;

                // Act
                var result = await userService.GetFullNameByIdAsync(userId.ToString());                // Assert
                Assert.IsNotNull(result);
                Assert.AreEqual("Ivan Ivanov", result);
            }
        }

        
    }
}