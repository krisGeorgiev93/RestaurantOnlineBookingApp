using Microsoft.EntityFrameworkCore;
using RestaurantOnlineBooking.Services.Data.Interfaces;
using RestaurantOnlineBookingApp.Data;
using RestaurantOnlineBookingApp.Data.Models;
using RestaurantOnlineBookingApp.Web.ViewModels.Home;
using RestaurantOnlineBookingApp.Web.ViewModels.Restaurant;

namespace RestaurantOnlineBooking.Services.Data
{
    public class RestaurantService : IRestaurantService
    {
        private readonly RestaurantBookingDbContext dBContext;

        public RestaurantService(RestaurantBookingDbContext dBContext)
        {
            this.dBContext = dBContext;
        }

        public async Task CreateRestaurantAsync(RestaurantFormModel model, string ownerId)
        {
            Restaurant restaurant = new Restaurant()
            {
                Name = model.Name,
                Description = model.Description,
                Address = model.Address,
                Rating = model.Rating,
                ImageUrl = model.ImageUrl,
                Capacity = model.Capacity,
                CategoryId = model.CategoryId,
                CityId = model.CityId,
                OwnerId = Guid.Parse(ownerId)
            };
            await this.dBContext.Restaurants.AddAsync(restaurant);
            await this.dBContext.SaveChangesAsync();
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
                    Rating = r.Rating,
                    Capacity = r.Capacity,
                    City = r.City.CityName
                })
                .ToListAsync();
        }
    }
}
