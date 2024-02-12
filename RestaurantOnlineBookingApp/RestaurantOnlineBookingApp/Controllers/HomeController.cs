
namespace RestaurantOnlineBookingApp.Controllers
{

    using System.Diagnostics;

    using Microsoft.AspNetCore.Mvc;
    using RestaurantOnlineBooking.Services.Data.Interfaces;
    using RestaurantOnlineBookingApp.Web.ViewModels.Home;

    public class HomeController : Controller
    {
        private readonly IRestaurantService _restaurantService;
        private readonly IOwnerService _ownerService;
        private readonly ICategoryService _categoryService;

        public HomeController(IRestaurantService restaurantService, IOwnerService ownerService, ICategoryService categoryService)
        {
            _restaurantService = restaurantService;
            _ownerService = ownerService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var model =  await _restaurantService.GetAllAsync();
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
