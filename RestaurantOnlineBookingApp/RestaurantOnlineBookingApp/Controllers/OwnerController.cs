using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantOnlineBooking.Services.Data.Interfaces;
using System.Security.Claims;

using static RestaurantOnlineBookingApp.Common.NotificationMessages;

namespace RestaurantOnlineBookingApp.Web.Controllers
{
    [Authorize]
    public class OwnerController : Controller
    {
        private readonly IOwnerService _ownerService;

        public OwnerController(IOwnerService ownerService)
        {
            _ownerService = ownerService;
        }

        [HttpGet]
        public async Task<IActionResult> Join()
        {
            string? userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            bool isOwner = await this._ownerService.OwnerExistById(userId);
            if (isOwner)
            {
                TempData[ErrorMessage] = "You are already owner!";

                return RedirectToAction("Index", "Home");
            }
            return this.View();
        }
    }
}
