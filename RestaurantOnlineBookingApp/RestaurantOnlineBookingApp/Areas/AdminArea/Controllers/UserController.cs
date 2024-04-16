using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using RestaurantOnlineBooking.Services.Data;
using RestaurantOnlineBooking.Services.Data.Interfaces;
using RestaurantOnlineBookingApp.Data;
using RestaurantOnlineBookingApp.Data.Models;
using RestaurantOnlineBookingApp.Web.ViewModels.Category;
using RestaurantOnlineBookingApp.Web.ViewModels.User;
using static RestaurantOnlineBookingApp.Common.ApplicationConstants;
using static RestaurantOnlineBookingApp.Common.NotificationMessages;


namespace RestaurantOnlineBookingApp.Web.Areas.AdminArea.Controllers
{
    public class UserController : BaseAdminController
    {
        private readonly IUserService userService;
        private readonly IMemoryCache memoryCache;
        private readonly RestaurantBookingDbContext dbContext;
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<IdentityRole<Guid>> roleManager;
        private ICategoryService categoryService;
        public UserController(IUserService userService, IMemoryCache memoryCache,
            RestaurantBookingDbContext dbContext, UserManager<AppUser> userManager, RoleManager<IdentityRole<Guid>> roleManager, ICategoryService categoryService)
        {
            this.userService = userService;
            this.memoryCache = memoryCache;
            this.dbContext = dbContext;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.categoryService = categoryService;
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
            int restaurantCount = await dbContext.Restaurants
                .Where(r=> r.IsActive)
                .CountAsync();
            ViewBag.RestaurantCount = restaurantCount;
            return View("RestaurantCount"); 
        }
        public async Task<IActionResult> UserCount()
        {
            int userCount = await dbContext.Users.CountAsync();
            ViewBag.UserCount = userCount;
            return View("UserCount");
        }

        [HttpGet]
        public IActionResult MakeAdmin()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> MakeAdmin(MakeAdminViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await userManager.FindByEmailAsync(model.UserEmail);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "User with this email does not exist.");
                return View(model);
            }

            var adminRoleExists = await roleManager.RoleExistsAsync("Admin");
            if (!adminRoleExists)
            {
                ModelState.AddModelError(string.Empty, "Admin role does not exist.");
                return View(model);
            }

            var userIsAlreadyAdmin = await userManager.IsInRoleAsync(user, "Admin");
            if (userIsAlreadyAdmin)
            {
                ModelState.AddModelError(string.Empty, "User is already an admin.");
                return View(model);
            }

            var result = await userManager.AddToRoleAsync(user, "Admin");
            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Failed to make user an admin.");
                return View(model);
            }

            return RedirectToAction(nameof(Success));
        }

        public IActionResult Success()
        {
            return View();
        }

        [HttpGet]
        public IActionResult RemoveAdmin()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RemoveAdmin(string userEmail)
        {
            var user = await userManager.FindByEmailAsync(userEmail);
            if (user == null)
            {
                return NotFound();
            }

            var adminRoleExists = await roleManager.RoleExistsAsync("Admin");
            if (!adminRoleExists)
            {
                return NotFound();
            }

            var userIsAdmin = await userManager.IsInRoleAsync(user, "Admin");
            if (!userIsAdmin)
            {
                this.TempData[ErrorMessage] = "This user is not in Admin Role!";
                return this.View();
            }

            var result = await userManager.RemoveFromRoleAsync(user, "Admin");
            if (!result.Succeeded)
            {
                return StatusCode(500);
            }

            this.TempData[SuccessMessage] = "Admin role has been successfully removed from the user.";
            return RedirectToAction("RemoveAdmin");
        }

        public IActionResult AllCategories()
        {
            var categories = dbContext.Categories.ToList();
            return View(categories);
        }

        public IActionResult AddCategory()
        {
            return View();
        }       

        [HttpPost]
        public async Task<IActionResult> AddCategory(AddCategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Проверка дали категорията вече съществува
            bool categoryExists = await categoryService.ExistByNameAsync(model.Name);
            if (categoryExists)
            {
                this.TempData[ErrorMessage] = "Category with this name already exists.";
                return View(model);
            }

            var category = new Category
            {
                Name = model.Name
            };

            dbContext.Categories.Add(category);
            await dbContext.SaveChangesAsync();

            return RedirectToAction("AllCategories", "User"); 
        }

        [HttpPost]
        public async Task<IActionResult> RemoveCategory(int id)
        {
            var category = await dbContext.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            // Проверка за наличие на ресторанти със съответната категория
            var restaurantsWithCategory = await dbContext.Restaurants.AnyAsync(r => r.CategoryId == id);
            if (restaurantsWithCategory)
            {
                TempData[ErrorMessage] = "Cannot delete category because there are restaurants associated with it.";
                return RedirectToAction("AllCategories", "User");
            }

            dbContext.Categories.Remove(category);
            await dbContext.SaveChangesAsync();

            return RedirectToAction("AllCategories", "User");
        }

    }
}
