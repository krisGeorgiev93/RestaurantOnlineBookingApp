using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantOnlineBooking.Services.Data;
using RestaurantOnlineBooking.Services.Data.Interfaces;
using RestaurantOnlineBooking.Services.Data.Models;
using RestaurantOnlineBookingApp.Data;
using RestaurantOnlineBookingApp.Web.ViewModels.Meal;
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
        private readonly IMealService _mealService;
        private readonly RestaurantBookingDbContext _dbContext;

        public RestaurantController(IOwnerService ownerService, ICategoryService categoryService,
            RestaurantBookingDbContext dbContext, IRestaurantService restaurantService, ICityService cityService, IMealService mealService)
        {
            _ownerService = ownerService;
            _categoryService = categoryService;
            _restaurantService = restaurantService;
            _cityService = cityService;
            _dbContext = dbContext;
            _mealService = mealService;
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
            try
            {
                RestaurantFormModel model = new RestaurantFormModel()
                {
                    Categories = await this._categoryService.GetAllCategoriesAsync(),
                    Cities = await this._cityService.GetAllCitiesAsync()
                };
                return View(model);
            }
            catch (Exception)
            {
                return this.Error();
            }            
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
                string? ownerId = 
                    await _ownerService.OwnerIdByUserIdAsync(this.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
              string restaurantId =
                    await this._restaurantService.CreateAndReturnRestaurantIdAsync(model, ownerId!);

                this.TempData[SuccessMessage] = "Restaurant was added successfully!";
                return RedirectToAction("Details", "Restaurant", new { id = restaurantId });

            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty, "Unexpected error! Please try again later or contact administrator!");
                model.Categories = await this._categoryService.GetAllCategoriesAsync();

                return View(model);
            }

        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(string id)
        {
            bool restaurantExists = await this._restaurantService.RestaurantExistsByIdAsync(id);
            if (!restaurantExists)
            {
                this.TempData[ErrorMessage] = "Restaurant with this id does not exist!";
                return this.RedirectToAction("All", "Restaurant");
            }

            try
            {
                RestaurantDetailsViewModel model = await this._restaurantService
                .GetDetailsByIdAsync(id);

                return View(model);
            }
            catch (Exception)
            {
                return this.Error();
            }
            
        }

        [HttpGet]
        public async Task<IActionResult> Mine()
        {
            List<RestaurantAllViewModel> myRestaurants = new List<RestaurantAllViewModel>();

            string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            bool isUserOwner = await this._ownerService.OwnerExistByIdAsync(userId);

            try
            {
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
            catch (Exception)
            {
                return Error();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            bool restaurantExists = await this._restaurantService.RestaurantExistsByIdAsync(id);
            if (!restaurantExists)
            {
                this.TempData[ErrorMessage] = "Restaurant with this id does not exist!";
                return this.RedirectToAction("All", "Restaurant");
            }

            bool isUserOwner = await this._ownerService
                .OwnerExistByIdAsync(GetUserId()!);

            if (!isUserOwner)
            {
                this.TempData[ErrorMessage] = "Only owners can edit the restaurant information!";

                return this.RedirectToAction("Join", "Owner");
            }

            string? ownerId = await this._ownerService.OwnerIdByUserIdAsync(this.GetUserId()!);

            bool isOwnerOwnedRestaurant = await this._restaurantService
                .IsOwnerWithIdOwnedRestaurantWithIdAsync(id, ownerId!);

            if (!isOwnerOwnedRestaurant)
            {
                this.TempData[ErrorMessage] = "You have to be owner of the restaurant you want to edit"!;

                return this.RedirectToAction("Mine", "Restaurant");
            }

            try
            {
                RestaurantFormModel model = await this._restaurantService
                .GetRestaurantForEditByIdAsync(id);

                model.Categories = await this._categoryService.GetAllCategoriesAsync();
                model.Cities = await this._cityService.GetAllCitiesAsync();

                return this.View(model);
            }
            catch (Exception)
            {
                this.TempData[ErrorMessage] = "Unexpected error";
                return this.RedirectToAction("Index", "Home");
            }           

        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, RestaurantFormModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Cities = await this._cityService.GetAllCitiesAsync();
                model.Categories = await this._categoryService.GetAllCategoriesAsync();
                return this.View(model);
            }
            bool restaurantExists = await this._restaurantService.RestaurantExistsByIdAsync(id);
            if (!restaurantExists)
            {
                this.TempData[ErrorMessage] = "Restaurant with this id does not exist!";
                return this.RedirectToAction("All", "Restaurant");
            }

            bool isUserOwner = await this._ownerService
                .OwnerExistByIdAsync(GetUserId()!);

            if (!isUserOwner)
            {
                this.TempData[ErrorMessage] = "Only owners can edit the restaurant information!";

                return this.RedirectToAction("Join", "Owner");
            }

            string? ownerId = await this._ownerService.OwnerIdByUserIdAsync(this.GetUserId()!);

            bool isOwnerOwnedRestaurant = await this._restaurantService
                .IsOwnerWithIdOwnedRestaurantWithIdAsync(id, ownerId!);

            if (!isOwnerOwnedRestaurant)
            {
                this.TempData[ErrorMessage] = "You have to be owner of the restaurant you want to edit"!;

                return this.RedirectToAction("Mine", "Restaurant");
            }

            try
            {
                await this._restaurantService.EditRestaurantByIdAsync(id, model);
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty, "Unexpected error");
                model.Categories = await this._categoryService.GetAllCategoriesAsync();
                model.Cities = await this._cityService.GetAllCitiesAsync();

                return this.View(model);
            }

            this.TempData[SuccessMessage] = "Restaurant was edited successfully!";
            return this.RedirectToAction("Details", "Restaurant", new {id = id});
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            bool restaurantExists = await this._restaurantService.RestaurantExistsByIdAsync(id);
            if (!restaurantExists)
            {
                this.TempData[ErrorMessage] = "Restaurant with this id does not exist!";
                return this.RedirectToAction("All", "Restaurant");
            }

            bool isUserOwner = await this._ownerService
                .OwnerExistByIdAsync(GetUserId()!);

            if (!isUserOwner)
            {
                this.TempData[ErrorMessage] = "Only owners can edit the restaurant information!";

                return this.RedirectToAction("Join", "Owner");
            }

            string? ownerId = await this._ownerService.OwnerIdByUserIdAsync(this.GetUserId()!);

            bool isOwnerOwnedRestaurant = await this._restaurantService
                .IsOwnerWithIdOwnedRestaurantWithIdAsync(id, ownerId!);

            if (!isOwnerOwnedRestaurant)
            {
                this.TempData[ErrorMessage] = "You have to be owner of the restaurant you want to edit"!;

                return this.RedirectToAction("Mine", "Restaurant");
            }

            try
            {
                RestaurantDeleteDetailsViewModel model = 
                    await this._restaurantService.GetRestaurantForDeleteByIdAsync(id);

                return this.View(model);
            }
            catch (Exception)
            {
                return this.Error();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(RestaurantDeleteDetailsViewModel model,string id)
        {
            bool restaurantExists = await this._restaurantService.RestaurantExistsByIdAsync(id);
            if (!restaurantExists)
            {
                this.TempData[ErrorMessage] = "Restaurant with this id does not exist!";
                return this.RedirectToAction("All", "Restaurant");
            }

            bool isUserOwner = await this._ownerService
                .OwnerExistByIdAsync(GetUserId()!);

            if (!isUserOwner)
            {
                this.TempData[ErrorMessage] = "Only owners can edit the restaurant information!";

                return this.RedirectToAction("Join", "Owner");
            }

            string? ownerId = await this._ownerService.OwnerIdByUserIdAsync(this.GetUserId()!);

            bool isOwnerOwnedRestaurant = await this._restaurantService
                .IsOwnerWithIdOwnedRestaurantWithIdAsync(id, ownerId!);

            if (!isOwnerOwnedRestaurant)
            {
                this.TempData[ErrorMessage] = "You have to be owner of the restaurant you want to edit"!;

                return this.RedirectToAction("Mine", "Restaurant");
            }

            try
            {
                await this._restaurantService.DeleteRestaurantByIdAsync(id);

                this.TempData[WarningMessage] = "The restaurant was successfully deleted!";
                return this.RedirectToAction("Mine", "Restaurant");
            }
            catch (Exception)
            {
                return this.Error();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Menu(string restaurantId)
        {
            var restaurant = await _restaurantService.GetRestaurantByIdAsync(restaurantId);
            if (restaurant == null)
            {
                return NotFound();
            }

            try
            {
                var meals = await _mealService.GetAllMealsForRestaurantByIdAsync(restaurantId);
                var mealViewModels = meals.Select(m => new MealAllViewModel
                {
                    Id = m.Id.ToString(),
                    Name = m.Name,
                    Description = m.Description,
                    ImageUrl = m.ImageUrl,
                    Price = m.Price
                });

                return View(mealViewModels);
            }
            catch (Exception)
            {
                return this.Error();
            }
            
        }


        private IActionResult Error()
        {
            this.TempData[ErrorMessage] = "Unexpected error";
            return this.RedirectToAction("Index", "Home");
        }

        // Get currently logged-in user's Id
        private string GetUserId()
           => User.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}
