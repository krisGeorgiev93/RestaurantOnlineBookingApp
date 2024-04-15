namespace RestaurantOnlineBooking.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using RestaurantOnlineBooking.Services.Data.Interfaces;
    using RestaurantOnlineBookingApp.Data;
    using RestaurantOnlineBookingApp.Data.Models;
    using RestaurantOnlineBookingApp.Web.ViewModels.User;
    using System;
    using System.Collections.Generic;

    public class UserService : IUserService
    {
        private readonly RestaurantBookingDbContext dbContext;

        public UserService(RestaurantBookingDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<UserViewModel>> AllAsync()
        {
            List<UserViewModel> allUsers = await this.dbContext
                .Users
                .Select(u => new UserViewModel()
                {
                    Id = u.Id.ToString(),
                    Email = u.Email,
                    FullName = u.FirstName + " " + u.LastName
                })
                .ToListAsync();
            foreach (UserViewModel user in allUsers)
            {
                Owner? owner = this.dbContext
                    .Owners
                    .FirstOrDefault(a => a.UserId.ToString() == user.Id);
                if (owner != null)
                {
                    user.PhoneNumber = owner.PhoneNumber;
                }
                else
                {
                    user.PhoneNumber = string.Empty;
                }
            }

            return allUsers;
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
            //var user = await this.dbContext.Users.FindAsync(userId);
            //if (string.IsNullOrEmpty(user.FirstName) || string.IsNullOrEmpty(user.LastName))
            //{
            //    return null;
            //}
            //return user.FirstName + " " + user.LastName;
            AppUser? user = await this.dbContext
               .Users
               .FirstOrDefaultAsync(u => u.Id.ToString() == userId);
            if (user == null)
            {
                return null;
            }

            return $"{user.FirstName} {user.LastName}";
        }






    }
}
