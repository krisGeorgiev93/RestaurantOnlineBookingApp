using Microsoft.AspNetCore.Mvc;

namespace RestaurantOnlineBookingApp.Web.Areas.AdminArea.Controllers
{
    public class HomeController : BaseAdminController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
