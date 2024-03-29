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
            string existingOwnerUserId = OwnerUser.Id.ToString();

            bool result = await this.ownerService.OwnerExistByIdAsync(existingOwnerUserId);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task OwnerExistsByUserIdAsyncShouldReturnTrueIfNotExists()
        {
            string existingAgentUserId = GuestUser.Id.ToString();

            bool result = await this.ownerService.OwnerExistByIdAsync(existingAgentUserId);

            Assert.IsFalse(result);
        }
    }
}