using Microsoft.AspNetCore.Mvc;
using RestaurantOnlineBooking.Services.Data.Interfaces;
using RestaurantOnlineBookingApp.Web.ViewModels.Meal;
using static RestaurantOnlineBookingApp.Common.NotificationMessages;

namespace RestaurantOnlineBookingApp.Web.Controllers
{
    public class MealController : Controller
    {
        private readonly IMealService _mealService;

        public MealController(IMealService mealService)
        {
            _mealService = mealService;
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
        public IActionResult AddToRestaurant(string restaurantId)
        {
            if (!Guid.TryParse(restaurantId, out Guid restaurantGuid))
            {
                // Handle invalid restaurantId
                // could return a bad request response or redirect to an error page
                return BadRequest("Invalid restaurantId");
            }

            var viewModel = new MealFormViewModel
            {
                // Initialize other properties as needed
                RestaurantId = restaurantGuid // Set the RestaurantId in the view model
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddToRestaurant(MealFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model); // Return the view with validation errors if model state is invalid
            }

            try
            {
                await _mealService.AddMealToRestaurantAsync(model.RestaurantId.ToString(), model);
                return RedirectToAction("Details", "Restaurant", new { id = model.RestaurantId });
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                return this.Error();
            }
        }


        private IActionResult Error()
        {
            this.TempData[ErrorMessage] = "Unexpected error";
            return this.RedirectToAction("Index", "Home");
        }
    }
}
