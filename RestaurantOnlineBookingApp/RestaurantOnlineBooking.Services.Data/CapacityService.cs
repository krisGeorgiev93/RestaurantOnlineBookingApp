
namespace RestaurantOnlineBooking.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using RestaurantOnlineBooking.Services.Data.Interfaces;
    using RestaurantOnlineBookingApp.Data;
    using RestaurantOnlineBookingApp.Data.Models;
    using System.Globalization;
    public class CapacityService : ICapacityService
    {
        private readonly RestaurantBookingDbContext _dbContext;

        public CapacityService(RestaurantBookingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddCapacitiesFor60DaysAsync(Guid restaurantId, int capacity, string startDateString)
        {
            // Преобразуване на датата от string към DateTime
            DateTime startDate;
            if (!DateTime.TryParseExact(startDateString, "MM-dd-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate))
            {
                throw new FormatException("Invalid Format");
            }

            var endDate = startDate.AddDays(59);

            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            {
                var existingCapacity = await this._dbContext.CapacitiesParDate
                    .FirstOrDefaultAsync(c => c.RestaurantId == restaurantId && c.Date == date);

                if (existingCapacity == null)
                {
                    var capacityPerDate = new CapacityPerDate
                    {
                        RestaurantId = restaurantId,
                        Date = date,
                        Capacity = capacity
                    };

                    this._dbContext.CapacitiesParDate.Add(capacityPerDate);
                }
            }
            await this._dbContext.SaveChangesAsync();
        }
    }
}
