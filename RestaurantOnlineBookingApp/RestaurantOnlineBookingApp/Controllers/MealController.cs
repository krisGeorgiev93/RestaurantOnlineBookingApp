

namespace RestaurantOnlineBookingApp.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using RestaurantOnlineBooking.Services.Data.Interfaces;
    using RestaurantOnlineBookingApp.Data.Models;
    using RestaurantOnlineBookingApp.Web.ViewModels.Meal;
    using System.Globalization;
    using System.Security.Claims;
    using static RestaurantOnlineBookingApp.Common.NotificationMessages;
    public class MealController : Controller
    {
        private readonly IMealService _mealService;
        private readonly IOwnerService _ownerService;
        private readonly IRestaurantService _restaurantService;
        public MealController(IMealService mealService, IOwnerService ownerService, IRestaurantService restaurantService)
        {
            _mealService = mealService;           
            _ownerService = ownerService;
            _restaurantService = restaurantService;
        }


        [HttpGet]
        public IActionResult Create()
        {
            var model = new MealFormViewModel(); // Create a new instance of the view model
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(MealFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await this._mealService.CreateAsync(model);

            return RedirectToAction("Mine", "Restaurant");
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var meals = await this._mealService.GetAllMealsAsync();

            return View(meals);

        }

        [HttpGet]
        public IActionResult AddMeal(string restaurantId)
        {
            if (!Guid.TryParse(restaurantId, out Guid restaurantGuid))
            {
                // Handle invalid restaurantId
                // could return a bad request response or redirect to an error page
                return BadRequest("Invalid restaurantId");
            }

            var viewModel = new MealFormViewModel
            {
                RestaurantId = restaurantGuid // Set the RestaurantId in the view model
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddMeal(MealFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model); // Return the view with validation errors if model state is invalid
            }

            try
            {
                await _mealService.AddMealToRestaurantAsync(model.RestaurantId.ToString(), model);
                // Redirect to the menu of the restaurant that the meal was added to
                return RedirectToAction("Menu", "Restaurant", new { restaurantId = model.RestaurantId });
            }
            catch (Exception)
            {
                // Log the exception or handle it appropriately
                return this.Error();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            bool mealExists = await this._mealService.MealExistsByIdAsync(id);

            if (!mealExists)
            {
                TempData[ErrorMessage] = "Meal with the provided id does not exist!";

                return RedirectToAction("Mine", "Restaurant");
            }

            bool isUserOwner = await this._ownerService.OwnerExistByIdAsync(GetUserId()!);

            if (!isUserOwner)
            {
                TempData[ErrorMessage] = "Only owners can delete the restaurant menu!";
            }

            string? ownerId = await this._ownerService.OwnerIdByUserIdAsync(this.GetUserId()!);


            var meal = await this._mealService.GetMealByIdAsync(id);

            if (meal == null)
            {
                TempData[ErrorMessage] = "Meal with the provided id does not exist!";
                return RedirectToAction("Mine", "Restaurant");
            }

            string mealRestaurantId = meal.RestaurantId.ToString();

            bool isOwnerOwnedRestaurant = await this._restaurantService
               .IsOwnerWithIdOwnedRestaurantWithIdAsync(mealRestaurantId, ownerId!);

            if (!isOwnerOwnedRestaurant)
            {
                this.TempData[ErrorMessage] = "You have to be owner of the restaurant to delete the menu"!;

                return this.RedirectToAction("Mine", "Restaurant");
            }

            try
            {
                var mealForDelete = await this._mealService.GetMealByIdAsync(id);
                if (mealForDelete == null)
                {
                    return RedirectToAction("Index", "Home");
                }

                var restaurantId = mealForDelete.RestaurantId;

                await this._mealService.DeleteMealAsync(id);
                this.TempData[SuccessMessage] = "Menu was deleted successfully!";
                return RedirectToAction("Menu", "Restaurant", new { restaurantId });
            }
            catch (Exception)
            {
                return Error();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            bool mealExists = await this._mealService.MealExistsByIdAsync(id);

            if (!mealExists)
            {
                TempData[ErrorMessage] = "Meal with the provided id does not exist!";

                return RedirectToAction("Mine", "Restaurant");
            }

            bool isUserOwner = await this._ownerService.OwnerExistByIdAsync(GetUserId()!);

            if (!isUserOwner)
            {
                TempData[ErrorMessage] = "Only owners can edit the restaurant menu information!";
            }

            string? ownerId = await this._ownerService.OwnerIdByUserIdAsync(this.GetUserId()!);
                        

            var meal = await this._mealService.GetMealByIdAsync(id);

            if (meal == null)
            {
                TempData[ErrorMessage] = "Meal with the provided id does not exist!";
                return RedirectToAction("Mine", "Restaurant");
            }

            string restaurantId = meal.RestaurantId.ToString();

            bool isOwnerOwnedRestaurant = await this._restaurantService
               .IsOwnerWithIdOwnedRestaurantWithIdAsync(restaurantId, ownerId!);

            if (!isOwnerOwnedRestaurant)
            {
                this.TempData[ErrorMessage] = "You have to be owner of the restaurant you want to edit"!;

                return this.RedirectToAction("Mine", "Restaurant");
            }
            try
            {
                var mealModel = new MealFormViewModel()
                {
                    Id = meal.Id,
                    Name = meal.Name,
                    Description = meal.Description,
                    Image = meal.Image,
                    Price = meal.Price,
                    RestaurantId = meal.RestaurantId
                };

                return View(mealModel);
            }
            catch (Exception)
            {
                this.TempData[ErrorMessage] = "Unexpected error";
                return this.RedirectToAction("Index", "Home");
            }
           
        }

        [HttpPost]
        public async Task<IActionResult> Edit(MealFormViewModel mealModel)
        {
            bool mealExists = await this._mealService.MealExistsByIdAsync(mealModel.Id.ToString());

            if (!mealExists)
            {
                TempData[ErrorMessage] = "Meal with the provided id does not exist!";

                return RedirectToAction("Mine", "Restaurant");
            }

            bool isUserOwner = await this._ownerService.OwnerExistByIdAsync(GetUserId()!);

            if (!isUserOwner)
            {
                TempData[ErrorMessage] = "Only owners can edit the restaurant menu information!";
            }

            string? ownerId = await this._ownerService.OwnerIdByUserIdAsync(this.GetUserId()!);

            string restaurantId = mealModel.RestaurantId.ToString();
            bool isOwnerOwnedRestaurant = await this._restaurantService
               .IsOwnerWithIdOwnedRestaurantWithIdAsync(restaurantId, ownerId!);

            if (!isOwnerOwnedRestaurant)
            {
                this.TempData[ErrorMessage] = "You have to be owner of the restaurant you want to edit"!;

                return this.RedirectToAction("Mine", "Restaurant");
            }

            if (!ModelState.IsValid)
            {
                return View(mealModel);
            }

            try
            {
                await this._mealService.EditMealAsync(mealModel);            
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty, "Unexpected error");
            }
            this.TempData[SuccessMessage] = "Menu was edited successfully!";          
            return RedirectToAction("Menu", "Restaurant", new { restaurantId });
        }      


        private string GetUserId()
           => User.FindFirstValue(ClaimTypes.NameIdentifier);


        private IActionResult Error()
        {
            this.TempData[ErrorMessage] = "Unexpected error";
            return this.RedirectToAction("Index", "Home");
        }
    }
}
