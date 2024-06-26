﻿namespace RestaurantOnlineBooking.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using RestaurantOnlineBooking.Services.Data.Interfaces;
    using RestaurantOnlineBookingApp.Data;
    using RestaurantOnlineBookingApp.Web.ViewModels.City;
    public class CityService : ICityService
    {
        private readonly RestaurantBookingDbContext dBContext;

        public CityService(RestaurantBookingDbContext dBContext)
        {
            this.dBContext = dBContext;
        }

        public async Task<IEnumerable<string>> AllCitiesNamesAsync()
        {
            IEnumerable<string> allCitiesNames = await dBContext
                 .Cities
                 .Select(c => c.CityName)
                 .ToArrayAsync();

            return allCitiesNames;
        }

        public async Task<bool> ExistByNameAsync(string cityName)
        {
            bool result = await dBContext.Cities
                .AnyAsync(c => c.CityName == cityName);
            return result;
        }

        public async Task<IEnumerable<SelectCityFormModel>> GetAllCitiesAsync()
        {
            var allCities = await this.dBContext.Cities
                .AsNoTracking()
                .Select(c => new SelectCityFormModel
                {
                    Id = c.Id,
                    Name = c.CityName
                })
                .ToListAsync();

            return allCities;
        }
    }
}
