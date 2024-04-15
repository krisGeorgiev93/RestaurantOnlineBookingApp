using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using RestaurantOnlineBooking.Services.Data.Interfaces;
using RestaurantOnlineBookingApp.Data;
using RestaurantOnlineBookingApp.Web.ViewModels.User;
using static RestaurantOnlineBookingApp.Common.ApplicationConstants;

namespace RestaurantOnlineBookingApp.Web.Areas.AdminArea.Controllers
{
    public class UserController : BaseAdminController
    {
        private readonly IUserService userService;
        private readonly IMemoryCache memoryCache;
        private readonly RestaurantBookingDbContext dbContext;
        public UserController(IUserService userService, IMemoryCache memoryCache,
            RestaurantBookingDbContext dbContext)
        {
            this.userService = userService;
            this.memoryCache = memoryCache;
            this.dbContext = dbContext;
        }

        [ResponseCache(Duration = 30, Location = ResponseCacheLocation.Client, NoStore = false)]
        public async Task<IActionResult> All()
        {
            IEnumerable<UserViewModel> users =
                this.memoryCache.Get<IEnumerable<UserViewModel>>(UsersCacheKey);
            if (users == null)
            {
                users = await this.userService.AllAsync();

                MemoryCacheEntryOptions cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan
                        .FromMinutes(5));

                this.memoryCache.Set(UsersCacheKey, users, cacheOptions);
            }

            return View(users);
        }


        public async Task<IActionResult> RestaurantCount()
        {
            int restaurantCount = await dbContext.Restaurants.CountAsync();
            ViewBag.RestaurantCount = restaurantCount;
            return View("RestaurantCount"); 
        }
        public async Task<IActionResult> UserCount()
        {
            int userCount = await dbContext.Users.CountAsync();
            ViewBag.UserCount = userCount;
            return View("UserCount");
        }
    }
}
