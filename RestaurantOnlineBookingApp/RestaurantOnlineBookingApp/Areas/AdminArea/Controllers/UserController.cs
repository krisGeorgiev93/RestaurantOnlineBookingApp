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

        [HttpPost]
        public async Task<IActionResult> Delete(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("User ID is required.");
            }

            if (!Guid.TryParse(userId, out Guid id))
            {
                return BadRequest("Invalid user ID format.");
            }

            var user = await dbContext.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            try
            {
                user.IsDeleted = true;
                await dbContext.SaveChangesAsync();
                memoryCache.Remove(UsersCacheKey);
                return RedirectToAction("All");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the user: {ex.Message}");
            }
        }
    }
}
