namespace RestaurantBookingApp.Services.Tests
{
    using Microsoft.EntityFrameworkCore;
    using RestaurantOnlineBooking.Services.Data;
    using RestaurantOnlineBooking.Services.Data.Interfaces;
    using RestaurantOnlineBookingApp.Data;
    using static DbSeeder;
    public class OwnerServiceTests
    {
        private DbContextOptions<RestaurantBookingDbContext> dbOptions;
        private RestaurantBookingDbContext dbContext;

        private IOwnerService ownerService;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            this.dbOptions = new DbContextOptionsBuilder<RestaurantBookingDbContext>()
                .UseInMemoryDatabase("RestaurantBookingInMemory" + Guid.NewGuid().ToString())
                .Options;
            this.dbContext = new RestaurantBookingDbContext(this.dbOptions);

            this.dbContext.Database.EnsureCreated();
            SeedDatabase(this.dbContext);

            this.ownerService = new OwnerService(this.dbContext);
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task OwnerExistsByUserIdAsyncShouldReturnTrueIfExists()
        {
            // Arrange
            string existingOwnerUserId = OwnerUser.Id.ToString();

            // Act
            bool result = await this.ownerService.OwnerExistByIdAsync(existingOwnerUserId);

            //Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task OwnerExistsByUserIdAsyncShouldReturnFalseIfNotExists()
        {
            // Arrange
            string existingOwnerUserId = GuestUser.Id.ToString();

            // Act
            bool result = await this.ownerService.OwnerExistByIdAsync(existingOwnerUserId);

            //Assert
            Assert.IsFalse(result);
        }

        [Test]
        public async Task OwnerExistsByPhoneNumberAsyncShouldReturnTrueIfExists()
        {
            // Arrange
            string existingOwnerPhoneNumber = Owner.PhoneNumber.ToString();

            // Act
            bool result = await this.ownerService.OwnerExistsByPhoneNumberAsync(existingOwnerPhoneNumber);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task OwnerExistsByPhoneNumberAsyncShouldReturnFalseIfNotExists()
        {
            // Arrange
            string nonExistingOwnerPhoneNumber = "1234567890";
            // Act
            bool result = await this.ownerService.OwnerExistsByPhoneNumberAsync(nonExistingOwnerPhoneNumber);
            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public async Task OwnerIdByUserIdAsyncShouldReturnOwnerIdIfExists()
        {
            // Arrange
            string existingUserId = DbSeeder.OwnerUser.Id.ToString();

            // Act
            string ownerId = await this.ownerService.OwnerIdByUserIdAsync(existingUserId);

            // Assert
            Assert.IsNotNull(ownerId);
        }

        [Test]
        public async Task OwnerIdByUserIdAsyncShouldReturnNullIfUserDoesNotExist()
        {
            // Arrange
            string nonExistingUserId = "nonExistingUserId";

            // Act
            string ownerId = await this.ownerService.OwnerIdByUserIdAsync(nonExistingUserId);

            // Assert
            Assert.IsNull(ownerId);
        }
    }
}