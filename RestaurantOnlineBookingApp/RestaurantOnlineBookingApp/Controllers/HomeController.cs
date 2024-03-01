
namespace RestaurantOnlineBookingApp.Controllers
{

    using System.Diagnostics;

    using Microsoft.AspNetCore.Mvc;
    using RestaurantOnlineBooking.Services.Data.Interfaces;
    using RestaurantOnlineBookingApp.Web.ViewModels.Home;

    public class HomeController : Controller
    {
        private readonly IRestaurantService _restaurantService;

        public HomeController(IRestaurantService restaurantService, IOwnerService ownerService, ICategoryService categoryService)
        {
            _restaurantService = restaurantService;
        }

        public async Task<IActionResult> Index()
        {
            var model =  await _restaurantService.GetAllAsync();
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int statusCode)
        {
            if (statusCode == 400 || statusCode == 404)
            {
                return View("Error404");
            }

            if (statusCode == 401)
            {
                return View("Error401");
            }

            return View();
        }
    }
}
