using Microsoft.AspNetCore.Mvc;
using RestaurantOnlineBooking.Services.Data.Interfaces;
using RestaurantOnlineBookingApp.Data;
using RestaurantOnlineBookingApp.Web.ViewModels.Meal;

namespace RestaurantOnlineBookingApp.Web.Controllers
{
    public class MealController : Controller
    {
        private readonly IMealService _mealService;

        private readonly RestaurantBookingDbContext dbContext;

        public MealController(IMealService mealService, RestaurantBookingDbContext dbContext)
        {
            _mealService = mealService;
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new MealFormModel(); // Create a new instance of the view model
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(MealFormModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await this._mealService.Create(model);

            return RedirectToAction("All", "Restaurant");
        }
    }
}
