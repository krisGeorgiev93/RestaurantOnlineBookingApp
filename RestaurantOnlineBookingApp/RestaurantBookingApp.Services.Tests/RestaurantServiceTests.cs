namespace RestaurantBookingApp.Services.Tests
{
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using RestaurantOnlineBooking.Services.Data;
    using RestaurantOnlineBooking.Services.Data.Interfaces;
    using RestaurantOnlineBookingApp.Data;
    using RestaurantOnlineBookingApp.Data.Models;
    using RestaurantOnlineBookingApp.Web.ViewModels.Home;
    using RestaurantOnlineBookingApp.Web.ViewModels.Restaurant;
    using static DbSeeder;

    public class RestaurantServiceTests
    {
        private DbContextOptions<RestaurantBookingDbContext> dbOptions;
        private RestaurantBookingDbContext dbContext;

        private IRestaurantService restaurantService;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            this.dbOptions = new DbContextOptionsBuilder<RestaurantBookingDbContext>()
                .UseInMemoryDatabase("RestaurantBookingInMemory" + Guid.NewGuid().ToString())
                .Options;
            this.dbContext = new RestaurantBookingDbContext(this.dbOptions);

            this.dbContext.Database.EnsureCreated();
            SeedDatabase(this.dbContext);

            // Mock IPhotoService
            var photoServiceMock = new Mock<IPhotoService>();

            this.restaurantService = new RestaurantService(this.dbContext, photoServiceMock.Object);
        }

        [SetUp]
        public void Setup()
        {
        }
       
        [Test]
        public async Task AllByOwnerIdAsync_ReturnsCorrectCountOfRestaurants()
        {
            // Arrange
            var ownerId = DbSeeder.Owner1.Id.ToString();

            // Act
            var result = await restaurantService.AllByOwnerIdAsync(ownerId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IEnumerable<RestaurantAllViewModel>>(result);

            var resultList = result.ToList();
            Assert.AreEqual(1, resultList.Count);

            var restaurantViewModel = resultList.First();
            Assert.AreEqual(DbSeeder.Restaurant.Id.ToString(), restaurantViewModel.Id);
            Assert.AreEqual(DbSeeder.Restaurant.Name, restaurantViewModel.Name);
            Assert.AreEqual(DbSeeder.Restaurant.Description, restaurantViewModel.Description);
            Assert.AreEqual(DbSeeder.Restaurant.Address, restaurantViewModel.Address);
            Assert.AreEqual(DbSeeder.Restaurant.ImageUrl, restaurantViewModel.ImageUrl);
            Assert.AreEqual(DbSeeder.Restaurant.Capacity, restaurantViewModel.Capacity);
        }

        [Test]
        public async Task DeleteRestaurantByIdAsync_MarksRestaurantAsInactive()
        {
            // Arrange
            var restaurantId = DbSeeder.Restaurant.Id.ToString();

            // Act
            await restaurantService.DeleteRestaurantByIdAsync(restaurantId);

            // Assert
            var deletedRestaurant = await dbContext.Restaurants.FindAsync(DbSeeder.Restaurant.Id);
            Assert.IsNotNull(deletedRestaurant);
            Assert.IsFalse(deletedRestaurant.IsActive);
        }

        [Test]
        public async Task GetRestaurantByIdAsync_WithValidId_ShouldReturnRestaurant()
        {
            // Arrange
            var expectedRestaurant = DbSeeder.Restaurant;
            var restaurantId = expectedRestaurant.Id.ToString();

            // Act
            var result = await this.restaurantService.GetRestaurantByIdAsync(restaurantId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedRestaurant.Id, result.Id);
            Assert.AreEqual(expectedRestaurant.Name, result.Name);
            Assert.AreEqual(expectedRestaurant.Address, result.Address);
            Assert.AreEqual(expectedRestaurant.Description, result.Description);
            Assert.AreEqual(expectedRestaurant.StartingTime, result.StartingTime);
            Assert.AreEqual(expectedRestaurant.EndingTime, result.EndingTime);
            Assert.AreEqual(expectedRestaurant.ImageUrl, result.ImageUrl);
            Assert.AreEqual(expectedRestaurant.Capacity, result.Capacity);
            Assert.AreEqual(expectedRestaurant.IsActive, result.IsActive);
            Assert.AreEqual(expectedRestaurant.CityId, result.CityId);
            Assert.AreEqual(expectedRestaurant.CategoryId, result.CategoryId);
            Assert.AreEqual(expectedRestaurant.OwnerId, result.OwnerId);
        }

        [Test]
        public async Task GetRestaurantByIdAsync_WithInvalidId_ShouldReturnNull()
        {
            // Arrange
            var invalidRestaurantId = "InvalidId";

            // Act
            var result = await this.restaurantService.GetRestaurantByIdAsync(invalidRestaurantId);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task GetAllAsyncShouldReturnAllRestaurants()
        {            
            var result = await restaurantService.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.IsInstanceOf<IEnumerable<AllRestaurantsViewModel>>(result);
        }

        [Test]
        public async Task GetAllAsyncShouldReturnEmptyListWhenNoRestaurantsAreActive()
        {
            // Arrange            
            foreach (var restaurant in this.dbContext.Restaurants)
            {
                restaurant.IsActive = false;
            }
            await this.dbContext.SaveChangesAsync();

            // Act
            var result = await this.restaurantService.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.IsEmpty(result);
        }


        [Test]
        public async Task GetRestaurantForDeleteByIdAsyncShouldReturnRestaurantDetailsForValidRestaurantId()
        {
            // Arrange
            var restaurantId = Guid.NewGuid().ToString();
            var expectedRestaurant = new Restaurant
            {
                Id = Guid.Parse(restaurantId),
                Name = "Test",
                Description = "Test",
                Address = "Test",
                ImageUrl = "testimage",
                IsActive = true
            };

            this.dbContext.Restaurants.Add(expectedRestaurant);
            await this.dbContext.SaveChangesAsync();

            // Act
            var result = await this.restaurantService.GetRestaurantForDeleteByIdAsync(restaurantId);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(expectedRestaurant.Name, result.Name);
            Assert.AreEqual(expectedRestaurant.Description, result.Description);
            Assert.AreEqual(expectedRestaurant.Address, result.Address);
            Assert.AreEqual(expectedRestaurant.ImageUrl, result.ImageUrl);
        }

        [Test]
        public async Task GetRestaurantForDeleteByIdAsyncShouldReturnNullForNonExistentRestaurantId()
        {
            // Arrange
            var nonExistentRestaurantId = Guid.NewGuid().ToString();

            // Act
            var result = await this.restaurantService.GetRestaurantForDeleteByIdAsync(nonExistentRestaurantId);

            // Assert
            Assert.Null(result);
        }

        [Test]
        public async Task GetRestaurantForEditByIdAsyncShouldThrowExceptionForNonExistentRestaurantId()
        {
            // Arrange
            var nonExistentRestaurantId = Guid.NewGuid().ToString();

            // Act & Assert
            Assert.ThrowsAsync<InvalidOperationException>(async () => await this.restaurantService.GetRestaurantForEditByIdAsync(nonExistentRestaurantId));
        }
                

        [Test]
        public async Task RestaurantExistsByIdAsyncShouldReturnFalseWhenRestaurantDoesNotExist()
        {
            // Arrange
            var nonExistentRestaurantId = Guid.NewGuid().ToString();

            // Act
            var result = await this.restaurantService.RestaurantExistsByIdAsync(nonExistentRestaurantId);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public async Task AddRestaurantToFavoriteAsyncShouldAddRestaurantToUserFavorites()
        {
            // Arrange
            var userId = Guid.NewGuid().ToString();
            var restaurantId = Guid.NewGuid();

            // Act
            await this.restaurantService.AddRestaurantToFavoriteAsync(userId, restaurantId);

            // Assert
            var userFavorite = await this.dbContext.UserFavoriteRestaurants
                .FirstOrDefaultAsync(ufr => ufr.UserId == new Guid(userId) && ufr.RestaurantId == restaurantId);

            Assert.IsNotNull(userFavorite);
        }

       
    }
}