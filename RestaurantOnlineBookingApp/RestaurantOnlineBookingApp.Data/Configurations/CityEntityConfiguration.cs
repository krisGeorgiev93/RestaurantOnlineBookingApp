using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantOnlineBookingApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantOnlineBookingApp.Data.Configurations
{
    public class CityEntityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.HasData(this.UploadCities());
        }

        private City[] UploadCities()
        {
            ICollection<City> cities = new HashSet<City>();

            City city;

            city = new City()
            {
                Id = 1,
                CityName = "Sofia"
            };
            cities.Add(city);

            city = new City()
            {
                Id = 2,
                CityName = "Plovdiv"
            };
            cities.Add(city);

            city = new City()
            {
                Id = 3,
                CityName = "Varna"
            };
            cities.Add(city);

            city = new City()
            {
                Id = 4,
                CityName = "Burgas"
            };
            cities.Add(city);

            return cities.ToArray();
        }

    }
}
