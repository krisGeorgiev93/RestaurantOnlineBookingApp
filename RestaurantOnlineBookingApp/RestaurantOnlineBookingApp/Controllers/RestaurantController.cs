using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantOnlineBooking.Services.Data;
using RestaurantOnlineBooking.Services.Data.Interfaces;
using RestaurantOnlineBooking.Services.Data.Models;
using RestaurantOnlineBookingApp.Data;
using RestaurantOnlineBookingApp.Web.ViewModels.Restaurant;
using System.Security.Claims;
using static RestaurantOnlineBookingApp.Common.NotificationMessages;

namespace RestaurantOnlineBookingApp.Web.Controllers
{
    [Authorize]
    public class RestaurantController : Controller
    {
        private readonly IOwnerService _ownerService;
        private readonly ICategoryService _categoryService;
        private readonly IRestaurantService _restaurantService;
        private readonly ICityService _cityService;
        private readonly RestaurantBookingDbContext _dbContext;

        public RestaurantController(IOwnerService ownerService, ICategoryService categoryService,
            RestaurantBookingDbContext dbContext, IRestaurantService restaurantService, ICityService cityService)
        {
            _ownerService = ownerService;
            _categoryService = categoryService;
            _restaurantService = restaurantService;
            _cityService = cityService;
            _dbContext = dbContext;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> All([FromQuery] AllRestaurantsQueryModel queryModel)
        {
            AllRestaurantsFilteredServiceModel serviceModel =
                await this._restaurantService.AllAsync(queryModel);

            queryModel.Restaurants = serviceModel.Restaurants;
            queryModel.Cities = await _cityService.AllCitiesNamesAsync();
            queryModel.Categories = await _categoryService.AllCategoryNamesAsync();
            queryModel.TotalRestaurants = serviceModel.TotalRestaurantsCount;
            return View(queryModel);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            //only owners can add restaurants 
            bool isOwner = await this._ownerService.OwnerExistByIdAsync(this.User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            if (!isOwner)
            {
                this.TempData[ErrorMessage] = "You must become an owner to add new restaurants!";

                return RedirectToAction("Join", "Owner");
            }

            RestaurantFormModel model = new RestaurantFormModel()
            {
                Categories = await this._categoryService.GetAllCategoriesAsync(),
                Cities = await this._cityService.GetAllCitiesAsync()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(RestaurantFormModel model)
        {
            bool isOwner = await this._ownerService.OwnerExistByIdAsync(this.User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            if (!isOwner)
            {
                this.TempData[ErrorMessage] = "You must become an owner to add new restaurants!";

                return RedirectToAction("Join", "Owner");
            }

            bool categoryExists = await this._categoryService.ExistByIdAsync(model.CategoryId);

            if (!categoryExists)
            {
                ModelState.AddModelError(nameof(model.CategoryId), "This category does not exist!");
            }

            if (!ModelState.IsValid)
            {
                model.Categories = await this._categoryService.GetAllCategoriesAsync();

                return this.View(model);
            }

            try
            {
                string? ownerId = await _ownerService.OwnerIdByUserIdAsync(this.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
                await this._restaurantService.CreateRestaurantAsync(model, ownerId!);
            }
            catch (Exception e)
            {

                this.ModelState.AddModelError(string.Empty, "Unexpected error! Please try again later or contact administrator!");
                model.Categories = await this._categoryService.GetAllCategoriesAsync();

                return View(model);
            }

            return RedirectToAction("All", "Restaurant");
        }

        [HttpGet]
        public async Task<IActionResult> Mine()
        {
            List<RestaurantAllViewModel> myRestaurants = new List<RestaurantAllViewModel>();

            string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            bool isUserOwner = await this._ownerService.OwnerExistByIdAsync(userId);

            if (isUserOwner)
            {
                string? ownerId = await this._ownerService.OwnerIdByUserIdAsync(userId);

                myRestaurants.AddRange(await this._restaurantService.AllByOwnerIdAsync(ownerId!));
            }
            else
            {
                myRestaurants.AddRange(await this._restaurantService.AllByUserIdAsync(userId));
            }

            return View(myRestaurants);
        }
    }
}
