using Microsoft.EntityFrameworkCore;
using RestaurantOnlineBooking.Services.Data.Interfaces;
using RestaurantOnlineBookingApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantOnlineBooking.Services.Data
{
    public class OwnerService : IOwnerService
    {

        private readonly RestaurantBookingDbContext dBContext;

        public OwnerService(RestaurantBookingDbContext dBContext)
        {
            this.dBContext = dBContext;
        }

        public async Task<bool> OwnerExistById(string id)
        {
            bool result = await this.dBContext
                .Owners
                .AnyAsync(o => o.UserId.ToString() == id);
                
            return result;

        }
    }
}
