using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RestaurantOnlineBookingApp.Web.Controllers
{
    [Authorize]
    public class OwnerController : Controller
    {
        public async Task<IActionResult> Join()
        {
            return View();
        }
    }
}
