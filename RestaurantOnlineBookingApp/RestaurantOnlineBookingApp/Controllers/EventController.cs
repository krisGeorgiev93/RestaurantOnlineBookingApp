using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantOnlineBooking.Services.Data;
using RestaurantOnlineBooking.Services.Data.Interfaces;
using RestaurantOnlineBookingApp.Web.ViewModels.Event;
using System.Security.Claims;
using static RestaurantOnlineBookingApp.Common.NotificationMessages;

namespace RestaurantOnlineBookingApp.Web.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventService eventService;
        private readonly IOwnerService ownerService;
        private readonly IRestaurantService restaurantService;
        public EventController(IEventService eventService, IOwnerService ownerService, IRestaurantService restaurantService)
        {
            this.eventService = eventService;
            this.ownerService = ownerService;
            this.restaurantService = restaurantService;
        }

        [HttpGet]
        public async Task<IActionResult> Create(string restaurantId)
        {
            if (!Guid.TryParse(restaurantId, out Guid restaurantGuid))
            {
                // Handle invalid restaurantId
                return BadRequest("Invalid restaurantId");
            }
            //only owners can add restaurants 
            bool isOwner = await this.ownerService.OwnerExistByIdAsync(this.User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            if (!isOwner)
            {
                this.TempData[ErrorMessage] = "You must become an owner to add new restaurants!";

                return RedirectToAction("Join", "Owner");
            }

            string? ownerId = await this.ownerService.OwnerIdByUserIdAsync(this.User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            bool isOwnerOwnedRestaurant = await this.restaurantService
                .IsOwnerWithIdOwnedRestaurantWithIdAsync(restaurantId, ownerId!);

            if (!isOwnerOwnedRestaurant)
            {
                this.TempData[ErrorMessage] = "You have to be owner of the restaurant if you want to add events"!;

                return this.RedirectToAction("Mine", "Restaurant");
            }
            var model = new EventFormModel()
            {
                RestaurantId = restaurantGuid
            };// Създавате инстанция на модела
            return View(model); // Подавате модела на изгледа
        }

        [HttpPost]
        public async Task<IActionResult> Create(EventFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var ownerId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var restaurantId = model.RestaurantId.ToString();

            try
            {
                await this.eventService.CreateEventAsync(model, restaurantId);
                return RedirectToAction("Index", "Home");
            }
            catch (ArgumentException)
            {
                this.Error();
                return View(model);
            }
        }
       
        private IActionResult Error()
        {
            this.TempData[ErrorMessage] = "Unexpected error";
            return this.RedirectToAction("Index", "Home");
        }
    }
}
