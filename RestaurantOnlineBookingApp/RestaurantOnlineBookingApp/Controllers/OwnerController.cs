using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantOnlineBooking.Services.Data.Interfaces;
using RestaurantOnlineBookingApp.Web.ViewModels.Owner;
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
            bool isOwner = await this._ownerService.OwnerExistByIdAsync(userId);
            if (isOwner)
            {
                TempData[ErrorMessage] = "You are already owner!";

                return RedirectToAction("Index", "Home");
            }
            return this.View();
        }

        public async Task<IActionResult> Join(JoinOwnerFormModel model)
        {
            string? userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            bool isOwner = await this._ownerService.OwnerExistByIdAsync(userId);
            if (isOwner)
            {
                TempData[ErrorMessage] = "You are already owner!";

                return RedirectToAction("Index", "Home");
            }

            bool isPhoneNumberTaken = await this._ownerService.OwnerExistsByPhoneNumberAsync(model.PhoneNumber);
            if (isPhoneNumberTaken)
            {
                this.ModelState.AddModelError(nameof(model.PhoneNumber), "Owner with this phone number already exist!");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await this._ownerService.Create(userId, model);
            }
            catch (Exception)
            {
                this.TempData[ErrorMessage] = "Unexpected error! Please try again later or contact administrator!";

                return RedirectToAction("Index", "Home");                 
            }

            return RedirectToAction("All", "Restaurant");
        }
    }
}
