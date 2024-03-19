using Microsoft.EntityFrameworkCore;
using RestaurantOnlineBooking.Services.Data.Interfaces;
using RestaurantOnlineBooking.Services.Data.Models;
using RestaurantOnlineBooking.Services.Data.Models.Statistics;
using RestaurantOnlineBookingApp.Data;
using RestaurantOnlineBookingApp.Data.Models;
using RestaurantOnlineBookingApp.Web.ViewModels.Home;
using RestaurantOnlineBookingApp.Web.ViewModels.Owner;
using RestaurantOnlineBookingApp.Web.ViewModels.Restaurant;
using System.Globalization;

namespace RestaurantOnlineBooking.Services.Data
{
    public class RestaurantService : IRestaurantService
    {
        private readonly RestaurantBookingDbContext dBContext;
        private readonly IUserService userService;
        private readonly IPhotoService photoService;
        public RestaurantService(RestaurantBookingDbContext dBContext, IUserService userService
            ,IPhotoService photoService)
        {
            this.dBContext = dBContext;
            this.userService = userService;
            this.photoService = photoService;
        }

        public async Task<AllRestaurantsFilteredServiceModel> AllAsync(AllRestaurantsQueryModel model)
        {
            // build expression tree only with 1 query

            IQueryable<Restaurant> restaurantQuery = dBContext.Restaurants.AsQueryable();

            
            if (!string.IsNullOrWhiteSpace(model.Category))
            {
                restaurantQuery = restaurantQuery.Where(r => r.Category.Name == model.Category);
            }

            if (!string.IsNullOrWhiteSpace(model.City))
            {
                restaurantQuery = restaurantQuery.Where(r => r.City.CityName == model.City);
            }

            if (!string.IsNullOrWhiteSpace(model.Search))
            {
                string wildCard = $"%{model.Search.ToLower()}%";

                restaurantQuery = restaurantQuery.Where(
                    r => EF.Functions.Like(r.Name, wildCard) ||
                    EF.Functions.Like(r.Description, wildCard) ||
                    EF.Functions.Like(r.Address, wildCard));
            }


            // Default sorting
            restaurantQuery = restaurantQuery.OrderBy(r => r.Id);

            // Check if sorting is requested
            switch (model.SortBy)
            {
                case SortOption.PriceAscending:
                    restaurantQuery = restaurantQuery.OrderBy(r => r.Meals.Average(m => m.Price));
                    break;
                case SortOption.PriceDescending:
                    restaurantQuery = restaurantQuery.OrderByDescending(r => r.Meals.Average(m => m.Price));
                    break;
                case SortOption.RatingAscending:
                    restaurantQuery = restaurantQuery.OrderBy(r => r.Reviews.Average(review => review.ReviewRating));
                    break;
                case SortOption.RatingDescending:
                    restaurantQuery = restaurantQuery.OrderByDescending(r => r.Reviews.Average(review => review.ReviewRating));
                    break;
                default:
                    // No sorting
                    break;
            }           

            IEnumerable<RestaurantAllViewModel> allRestaurants = await restaurantQuery
                .Where(r => r.IsActive)
                .Skip((model.CurrentPage - 1) * model.RestaurantsPerPage)
                .Take(model.RestaurantsPerPage)
                .Select(r => new RestaurantAllViewModel
                {
                    Id = r.Id.ToString(),
                    Name = r.Name,
                    Address = r.Address,
                    Description = r.Description,
                    ImageUrl = r.ImageUrl,
                    Capacity = r.Capacity,                              
                })
                .ToListAsync();

            int totalRestaurants = restaurantQuery.Count();

            return new AllRestaurantsFilteredServiceModel()
            {
                TotalRestaurantsCount = totalRestaurants,
                Restaurants = allRestaurants
            };
        }

        public async Task<IEnumerable<RestaurantAllViewModel>> AllByOwnerIdAsync(string ownerId)
        {
            IEnumerable<RestaurantAllViewModel> ownerRestaurants = await dBContext
                 .Restaurants
                 .Where(r => r.OwnerId.ToString() == ownerId && r.IsActive)
                 .Select(r => new RestaurantAllViewModel
                 {
                     Id = r.Id.ToString(),
                     Name = r.Name,
                     Address = r.Address,
                     Description = r.Description,
                     ImageUrl = r.ImageUrl,
                     Capacity = r.Capacity,
                 }).ToListAsync();

            return ownerRestaurants;
        }

        public async Task<IEnumerable<RestaurantAllViewModel>> AllByUserIdAsync(string userId)
        {
            IEnumerable<RestaurantAllViewModel> userRestaurants = await dBContext
                .Restaurants
                .Where(r => r.GuestId.ToString() == userId && r.IsActive && r.GuestId.HasValue)
                .Select(r => new RestaurantAllViewModel
                {
                    Id = r.Id.ToString(),
                    Name = r.Name,
                    Address = r.Address,
                    Description = r.Description,
                    ImageUrl = r.ImageUrl,
                    Capacity = r.Capacity,
                }).ToListAsync();
            return userRestaurants;
        }

        public async Task<string> CreateAndReturnRestaurantIdAsync(RestaurantFormModel model, string ownerId)
        {
            var photo = await this.photoService.AddPhotoAsync(model.Image);
            Restaurant restaurant = new Restaurant()
            {
                Name = model.Name,
                Description = model.Description,
                Address = model.Address,
                ImageUrl = photo.Url.ToString(),
                StartingTime = model.StartingTime,
                EndingTime = model.EndingTime,
                Capacity = model.Capacity,
                CategoryId = model.CategoryId,
                CityId = model.CityId,
                OwnerId = Guid.Parse(ownerId)
            };
            await this.dBContext.Restaurants.AddAsync(restaurant);
            await this.dBContext.SaveChangesAsync();

            // Извикване на метода за добавяне на капацитети за следващите 60 дни
            var capacityService = new CapacityService(this.dBContext);
            string formattedDate = DateTime.Now.ToString("MM-dd-yyyy");

            // Извикване на метода за добавяне на капацитетите за следващите 60 дни с форматираната дата
            await capacityService.AddCapacitiesFor60DaysAsync(restaurant.Id, model.Capacity, formattedDate);

            return restaurant.Id.ToString();
        }

        public async Task DeleteRestaurantByIdAsync(string restaurantId)
        {
            Restaurant restaurantToDelete = await this.dBContext
                 .Restaurants
                 .Where(r => r.IsActive)
                 .FirstAsync(r => r.Id.ToString() == restaurantId);

            restaurantToDelete.IsActive = false;
            // Изтриване на данните за капацитета за ресторанта
            var capacities = await this.dBContext.CapacitiesParDate
                .Where(c => c.RestaurantId == restaurantToDelete.Id)
                .ToListAsync();
            if (capacities.Any())
            {
                this.dBContext.CapacitiesParDate.RemoveRange(capacities);
            }

            await this.dBContext.SaveChangesAsync();
        }

        public async Task EditRestaurantByIdAsync(string restaurantId, RestaurantFormModel model)
        {
            Restaurant restaurant = await this.dBContext
                .Restaurants
                .Where(r => r.IsActive)
                .FirstAsync(r => r.Id.ToString() == restaurantId);

            var photo = await this.photoService.AddPhotoAsync(model.Image);

            restaurant.Name = model.Name;
            restaurant.Capacity = model.Capacity;
            restaurant.Address = model.Address;
            restaurant.ImageUrl = photo.Url.ToString();
            restaurant.CategoryId = model.CategoryId;
            restaurant.CityId = model.CityId;
            restaurant.StartingTime = model.StartingTime;
            restaurant.EndingTime = model.EndingTime;

            await this.dBContext.SaveChangesAsync();
        }

        public List<string> GenerateTimeSlots(TimeSpan startTime, TimeSpan endTime)
        {
            var timeSlots = new List<string>();
            var currentTime = startTime;

            while (currentTime <= endTime)
            {
                timeSlots.Add(currentTime.ToString("hh\\:mm tt")); // Format time as "hh:mm AM/PM"
                currentTime = currentTime.Add(new TimeSpan(0, 30, 0)); // Increment by 30 minutes
            }

            return timeSlots;
        }

        public async Task<IEnumerable<AllRestaurantsViewModel>> GetAllAsync()
        {
            return await dBContext
                .Restaurants
                .Select(r => new AllRestaurantsViewModel()
                {
                    Id = r.Id,
                    Name = r.Name,
                    Address = r.Address,
                    Description = r.Description,
                    ImageUrl = r.ImageUrl,
                    Capacity = r.Capacity,
                    City = r.City.CityName
                })
                .ToListAsync();
        }

        public async Task<RestaurantDetailsViewModel> GetDetailsByIdAsync(string restaurantId)
        {
            Restaurant? restaurant = await this.dBContext
                .Restaurants
                .Include(r => r.Category)
                .Include(r => r.City)
                .Include(r => r.Reviews) 
                .Include(r => r.Owner)                 
                .ThenInclude(r => r.User)               
                .Where(r => r.IsActive)
                .FirstAsync(r => r.Id.ToString() == restaurantId);

            double rating = restaurant.Reviews.Any() ? restaurant.Reviews.Average(r => r.ReviewRating) : 0;
            return new RestaurantDetailsViewModel()
            {
                Id = restaurant.Id.ToString(),
                Name = restaurant.Name,
                Description = restaurant.Description,
                Address = restaurant.Address,
                ImageUrl = restaurant.ImageUrl,
                Capacity = restaurant.Capacity,
                City = restaurant.City.CityName,
                Category = restaurant.Category.Name,
                OpeningTime = restaurant.StartingTime,
                ClosingTime = restaurant.EndingTime,
                OwnerInfo = new OwnerInfoOnRestaurantViewModel
                {
                    //FullName = this.userService.GetFullNameByIdAsync(restaurant.Owner.UserId.ToString()).ToString(),
                    PhoneNumber = restaurant.Owner.PhoneNumber,
                    Email = restaurant.Owner.User.Email                    
                },
                Rating = rating
            };
        }

        public async Task<Restaurant> GetRestaurantByIdAsync(string restaurantId)
        {
            // Retrieve the restaurant by its ID
            return await dBContext.Restaurants.FirstOrDefaultAsync(r => r.Id.ToString() == restaurantId);
        }

        public async Task<RestaurantDeleteDetailsViewModel> GetRestaurantForDeleteByIdAsync(string restaurantId)
        {
            Restaurant restaurant = await this.dBContext
                  .Restaurants
                  .Where(r => r.IsActive)
                  .FirstAsync(r => r.Id.ToString() == restaurantId);

            return new RestaurantDeleteDetailsViewModel()
            {
                Name = restaurant.Name,
                Description = restaurant.Description,
                Address = restaurant.Address,
                ImageUrl = restaurant.ImageUrl
            };
        }

        public async Task<RestaurantFormModel> GetRestaurantForEditByIdAsync(string restaurantId)
        {
            Restaurant restaurant = await this
                .dBContext
                .Restaurants
                .Include(r => r.Category)
                .Where(r => r.IsActive)
                .FirstAsync(r => r.Id.ToString() == restaurantId);

            return new RestaurantFormModel()
            {
                Name = restaurant.Name,
                Description = restaurant.Description,
                Address = restaurant.Address,
                Capacity = restaurant.Capacity,
                StartingTime = restaurant.StartingTime,
                EndingTime = restaurant.EndingTime,
                CityId = restaurant.CityId,
                CategoryId = restaurant.CategoryId
            };

        }

        public async Task<(TimeSpan startTime, TimeSpan endTime)> GetRestaurantOperatingHoursAsync(Guid restaurantId)
        {
            var restaurant = await this.dBContext.Restaurants.FindAsync(restaurantId);

            if (restaurant == null)
            {
                throw new ArgumentException("Invalid restaurant ID");
            }

            // Restaurant entity has properties for starting and ending times
            var startTime = restaurant.StartingTime;
            var endTime = restaurant.EndingTime;

            return (startTime, endTime);
        }

        public async Task<StatisticsServiceModel> GetStatisticsAsync()
        {
            return new StatisticsServiceModel()
            {
                TotalRestaurants = await this.dBContext.Restaurants.CountAsync(),
                TotalBookings = await this.dBContext.Restaurants.Where(r => r.GuestId.HasValue).CountAsync()
            };
        }

        public async Task<bool> IsOwnerWithIdOwnedRestaurantWithIdAsync(string restaurantId, string ownerId)
        {
            Restaurant restaurant = await this.dBContext
                .Restaurants
                .Where(r => r.IsActive)
                .FirstAsync(r => r.Id.ToString() == restaurantId);

            bool result = restaurant.OwnerId.ToString() == ownerId;

            return result;

        }

        public async Task<bool> RestaurantExistsByIdAsync(string restaurantId)
        {
            bool IsExists = await this.dBContext
                .Restaurants
                .Where(r => r.IsActive)
                .AnyAsync(r => r.Id.ToString() == restaurantId);

            return IsExists;
        }
    }

}
