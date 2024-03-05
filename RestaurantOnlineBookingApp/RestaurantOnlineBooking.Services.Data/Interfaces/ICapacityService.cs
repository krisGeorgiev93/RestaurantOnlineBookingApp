using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantOnlineBooking.Services.Data.Interfaces
{
    public interface ICapacityService
    {
        Task AddCapacitiesFor60DaysAsync(Guid restaurantId, int capacity, string startDateString);
    }
}
