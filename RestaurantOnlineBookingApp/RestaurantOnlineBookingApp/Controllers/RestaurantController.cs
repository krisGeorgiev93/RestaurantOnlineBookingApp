using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantOnlineBooking.Services.Data;
using RestaurantOnlineBookingApp.Data;

namespace RestaurantOnlineBookingApp.Web.Controllers
{
    [Authorize]
    public class RestaurantController : Controller
    {
        private readonly RestaurantService restaurantService;
        private readonly RestaurantBookingDbContext dbContext;

        public RestaurantController(RestaurantService restaurantService, RestaurantBookingDbContext dbContext)
        {
            this.restaurantService = restaurantService;
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var model = await restaurantService.GetAllAsync();
            return View(model);
        }
    }
}
