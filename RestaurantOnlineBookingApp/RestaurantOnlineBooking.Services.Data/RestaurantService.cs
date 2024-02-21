﻿using Microsoft.EntityFrameworkCore;
using RestaurantOnlineBooking.Services.Data.Interfaces;
using RestaurantOnlineBooking.Services.Data.Models;
using RestaurantOnlineBookingApp.Data;
using RestaurantOnlineBookingApp.Web.ViewModels.Home;
using RestaurantOnlineBookingApp.Web.ViewModels.Restaurant;
using System.Globalization;

namespace RestaurantOnlineBooking.Services.Data
{
    public class RestaurantService : IRestaurantService
    {
        private readonly RestaurantBookingDbContext dBContext;

        public RestaurantService(RestaurantBookingDbContext dBContext)
        {
            this.dBContext = dBContext;
        }

        public async Task<AllRestaurantsFilteredServiceModel> AllAsync(AllRestaurantsQueryModel model)
        {
            //we can build expression tree
            //only with 1 query
            IQueryable<RestaurantOnlineBookingApp.Data.Models.Restaurant> restaurantQuery = dBContext.Restaurants.AsQueryable();

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

            IEnumerable<RestaurantAllViewModel> allRestaurants = await restaurantQuery
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
                .ToArrayAsync();

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
                 .Where(r => r.OwnerId.ToString() == ownerId)
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
                .Where(r => r.GuestId.ToString() == userId)
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

        public async Task CreateRestaurantAsync(RestaurantFormModel model, string ownerId)
        {

            RestaurantOnlineBookingApp.Data.Models.Restaurant restaurant = new RestaurantOnlineBookingApp.Data.Models.Restaurant()
            {
                Name = model.Name,
                Description = model.Description,
                Address = model.Address,
                ImageUrl = model.ImageUrl,
                StartingTime = model.StartingTime,
                EndingTime = model.EndingTime,
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
                    Capacity = r.Capacity,
                    City = r.City.CityName
                })
                .ToListAsync();
        }
    }
}