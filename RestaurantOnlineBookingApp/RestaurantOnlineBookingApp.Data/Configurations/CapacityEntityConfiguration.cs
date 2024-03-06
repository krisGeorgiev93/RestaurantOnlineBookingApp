using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantOnlineBookingApp.Data.Models;

namespace RestaurantOnlineBookingApp.Data.Configurations
{
    public class CapacityEntityConfiguration : IEntityTypeConfiguration<CapacityPerDate>
    {

        private readonly RestaurantBookingDbContext _context;

        public CapacityEntityConfiguration(RestaurantBookingDbContext context)
        {
            _context = context;
        }
        public void Configure(EntityTypeBuilder<CapacityPerDate> builder)
        {
            builder.HasData(SeedCapacitiesFor60Days());

            builder.HasKey(c => new { c.RestaurantId, c.Date });

            builder.HasOne(c => c.Restaurant)
                   .WithMany(r => r.CapacityPerDates)
                   .HasForeignKey(c => c.RestaurantId)
                   .OnDelete(DeleteBehavior.Cascade);
        }

        private IEnumerable<CapacityPerDate> SeedCapacitiesFor60Days()
        {
            var capacities = new List<CapacityPerDate>();

            foreach (var restaurantData in GetRestaurantData())
            {
                var restaurant = new Restaurant
                {
                    Name = restaurantData.Name,
                    Address = restaurantData.Address,
                    Description = restaurantData.Description,
                    StartingTime = restaurantData.StartingTime,
                    EndingTime = restaurantData.EndingTime,
                    ImageUrl = restaurantData.ImageUrl,
                    Capacity = restaurantData.Capacity,
                    CityId = restaurantData.CityId,
                    CategoryId = restaurantData.CategoryId,
                    OwnerId = restaurantData.OwnerId
                };

                _context.Restaurants.Add(restaurant);

                var startDate = DateTime.Today;
                var endDate = startDate.AddDays(59);

                foreach (var date in EachDay(startDate, endDate))
                {
                    var capacityPerDate = new CapacityPerDate
                    {
                        RestaurantId = restaurant.Id,
                        Date = date,
                        Capacity = restaurant.Capacity
                    };

                    capacities.Add(capacityPerDate);
                }
            }

            return capacities;
        }

        private IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;
        }

        private IEnumerable<(string Name, string Address, string Description, TimeSpan StartingTime, TimeSpan EndingTime, string ImageUrl, int Capacity, int CityId, int CategoryId, Guid OwnerId)> GetRestaurantData()
        {
            yield return ("Turkish Restaurant", "Georgi Ivanov 26", "Best food from Turkey", new TimeSpan(12, 0, 0), new TimeSpan(23, 30, 0), "https://images.squarespace-cdn.com/content/v1/62f4e2e25f95bb5d35522adc/0c8e42d8-aa26-4c9e-acbc-bcfd336b3731/Havin_a_Turkish_X_socialawakening+18.jpg", 135, 3, 4, Guid.Parse("C4F8569C-1CDA-4B0B-94E4-16B44A4631CF"));
            yield return ("Asian Buffet", "Ivan Ivanov 26", "Best food from Asia", new TimeSpan(17, 0, 0), new TimeSpan(23, 30, 0), "https://cdn.vox-cdn.com/thumbor/Yb1U9a4hdQsC1iDQ_YIhJrqXL6g=/0x0:1024x682/1220x813/filters:focal(431x260:593x422):format(webp)/cdn.vox-cdn.com/uploads/chorus_image/image/59443047/makinoheader.0.jpg", 100, 1, 2, Guid.Parse("C4F8569C-1CDA-4B0B-94E4-16B44A4631CF"));
            yield return ("Best Of China", "Hristo Botev 76", "Best chinese in the country", new TimeSpan(18, 0, 0), new TimeSpan(23, 0, 0), "https://www.opentable.co.uk/blog/wp-content/uploads/sites/110/2020/02/sweetmandarin1.jpg", 50, 1, 6, Guid.Parse("C4F8569C-1CDA-4B0B-94E4-16B44A4631CF"));
            yield return ("Bulgarian Taste", "Hristo Hristov 74", "Traditional food from Bulgarian Kitchen", new TimeSpan(12, 0, 0), new TimeSpan(23, 45, 0), "https://media-cdn.tripadvisor.com/media/photo-s/03/b7/8b/ed/chevermeto-traditional.jpg", 150, 2, 1, Guid.Parse("C4F8569C-1CDA-4B0B-94E4-16B44A4631CF"));
        }
    }
}
