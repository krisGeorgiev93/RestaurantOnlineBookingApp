using Microsoft.EntityFrameworkCore;
using RestaurantOnlineBooking.Services.Data.Interfaces;
using RestaurantOnlineBookingApp.Data;
using RestaurantOnlineBookingApp.Data.Models;

namespace RestaurantOnlineBooking.Services.Data
{
    public class UserService : IUserService
    {
        private readonly RestaurantBookingDbContext dbContext;

        public UserService(RestaurantBookingDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<string> GetFullNameByEmailAsync(string email)
        {
            AppUser? user = await this.dbContext
               .Users
               .FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                return string.Empty;
            }

            return $"{user.FirstName} {user.LastName}";
        }
    }
}
