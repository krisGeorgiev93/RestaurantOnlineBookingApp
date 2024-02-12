using Microsoft.EntityFrameworkCore;
using RestaurantOnlineBooking.Services.Data.Interfaces;
using RestaurantOnlineBookingApp.Data;
using RestaurantOnlineBookingApp.Data.Models;
using RestaurantOnlineBookingApp.Web.ViewModels.Owner;
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

        public async Task Create(string userId, JoinOwnerFormModel model)
        {
            Owner owner = new Owner()
            {
                PhoneNumber = model.PhoneNumber,
                UserId = Guid.Parse(userId)
            };

            await dBContext.Owners.AddAsync(owner);
            await dBContext.SaveChangesAsync();

        }

       

        public async Task<bool> OwnerExistByIdAsync(string id)
        {
            bool result = await this.dBContext
                .Owners
                .AnyAsync(o => o.UserId.ToString() == id);
                
            return result;

        }

        public async Task<bool> OwnerExistsByPhoneNumberAsync(string phoneNumber)
        {
            bool result = await this.dBContext
                .Owners
                .AnyAsync(o => o.PhoneNumber == phoneNumber);

            return result;
        }

        public async Task<string> OwnerIdByUserIdAsync(string userId)
        {
            Owner? owner = await this.dBContext.Owners.FirstOrDefaultAsync(o => o.UserId.ToString() == userId);

            if (owner == null)
            {
                return null;
            }

            return owner.Id.ToString();
        }
    }
}
