namespace RestaurantOnlineBooking.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using RestaurantOnlineBooking.Services.Data.Interfaces;
    using RestaurantOnlineBookingApp.Data;
    using RestaurantOnlineBookingApp.Data.Models;
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

        public async Task<string> GetFullNameByIdAsync(string userId)
        {
            var user = await this.dbContext.Users.FindAsync(userId);
            if (string.IsNullOrEmpty(user.FirstName) || string.IsNullOrEmpty(user.LastName))
            {
                return null;
            }
            return user.FirstName + " " + user.LastName;
            //AppUser? user = await this.dbContext
            //   .Users
            //   .FirstOrDefaultAsync(u => u.Id.ToString() == userId);
            //if (user == null)
            //{
            //    return string.Empty;
            //}

            //return $"{user.FirstName} {user.LastName}";
        }
    }
}
