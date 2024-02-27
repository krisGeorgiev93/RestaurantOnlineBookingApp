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


        private IActionResult Error()
        {
            this.TempData[ErrorMessage] = "Unexpected error";
            return this.RedirectToAction("Index", "Home");
        }
    }
}
