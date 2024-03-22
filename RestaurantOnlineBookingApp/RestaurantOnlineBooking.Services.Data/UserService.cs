namespace RestaurantOnlineBooking.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using RestaurantOnlineBooking.Services.Data.Interfaces;
    using RestaurantOnlineBookingApp.Data;
    using RestaurantOnlineBookingApp.Data.Models;
    using System;
    using System.Collections.Generic;

    public class UserService : IUserService
    {
        private readonly RestaurantBookingDbContext dbContext;

        public UserService(RestaurantBookingDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddRestaurantToFavoriteAsync(string userId, Guid restaurantId)
        {
            var user = await dbContext.Users
                .Include(u => u.FavoriteRestaurants)
                .FirstOrDefaultAsync(u => u.Id.ToString() == userId);

            var restaurantToAdd = await dbContext.Restaurants.FindAsync(restaurantId);

            if (user != null && restaurantToAdd != null)
            {
                user.FavoriteRestaurants.Add(restaurantToAdd);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Restaurant>> GetFavoriteRestaurantsAsync(string userId)
        {
            var user = await dbContext.Users
                .Include(u => u.FavoriteRestaurants)
                .FirstOrDefaultAsync(u => u.Id.ToString() == userId);

            return user?.FavoriteRestaurants.ToList();
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
