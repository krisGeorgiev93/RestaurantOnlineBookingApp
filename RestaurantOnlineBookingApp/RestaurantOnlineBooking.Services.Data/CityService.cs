using Microsoft.EntityFrameworkCore;
using RestaurantOnlineBooking.Services.Data.Interfaces;
using RestaurantOnlineBookingApp.Data;
using RestaurantOnlineBookingApp.Web.ViewModels.City;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantOnlineBooking.Services.Data
{
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
