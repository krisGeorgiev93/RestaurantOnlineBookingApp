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
                CityName = "Sofia",
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/c/cc/Sofia_333.jpg/800px-Sofia_333.jpg"

            };
            cities.Add(city);

            city = new City()
            {
                Id = 2,
                CityName = "Plovdiv",
                ImageUrl = "https://i.guim.co.uk/img/media/1e6a94ecca5c1df6696f6673fe657e5d16819933/366_620_5634_3380/master/5634.jpg?width=1900&dpr=2&s=none"

            };
            cities.Add(city);

            city = new City()
            {
                Id = 3,
                CityName = "Varna",
                ImageUrl = "https://content.r9cdn.net/rimg/dimg/e5/ce/ad8df304-city-12778-1656216d7ab.jpg?width=2160&height=1215&xhint=2049&yhint=1054&crop=true&outputtype=webp"

            };
            cities.Add(city);

            city = new City()
            {
                Id = 4,
                CityName = "Burgas",
                ImageUrl = "https://www.kayak.co.uk/rimg/himg/33/36/89/expediav2-72370-760be8-606434.jpg?width=2160&height=1215&crop=true&outputtype=webp"

            };
            cities.Add(city);

            return cities.ToArray();
        }

    }
}
