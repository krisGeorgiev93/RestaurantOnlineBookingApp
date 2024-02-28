using Microsoft.AspNetCore.Mvc;

namespace RestaurantOnlineBookingApp.Web.Controllers
{
    public class EventController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
