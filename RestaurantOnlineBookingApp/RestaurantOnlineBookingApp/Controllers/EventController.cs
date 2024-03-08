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

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var @event = await this.eventService.GetEventByIdAsync(id);
            if (@event == null)
            {
                return NotFound("Event not found");
            }

            var eventFormModel = new EventFormModel
            {
                Id = @event.Id,
                Title = @event.Title,
                Date = @event.Date,
                Description = @event.Description,
                Price = @event.Price,
                ImageUrl = @event.ImageUrl
            };

            return View(eventFormModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EventFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await this.eventService.EditEventAsync(model);
                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("Unexpected error", ex.Message);
                return View(model);
            }
        }

        public async Task<IActionResult> AllByRestaurant(string restaurantId)
        {
            var events = await this.eventService.GetAllEventsByRestaurantIdAsync(restaurantId);
            var eventViewModels = events.Select(e => new EventViewModel
            {
                Id = e.Id.ToString(),
                Title = e.Title,
                Date = e.Date,
                Description = e.Description,
                Price = e.Price,
                ImageUrl = e.ImageUrl
            }).ToList();
            return View(eventViewModels);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteEvent(string eventId)
        {
            bool eventExists = await this.eventService.EventExistsByIdAsync(eventId);

            if (!eventExists)
            {
                TempData[ErrorMessage] = "Event with the provided id does not exist!";

                return RedirectToAction("AllByRestaurant");
            }

            bool isUserOwner = await this.ownerService.OwnerExistByIdAsync(GetUserId()!);

            if (!isUserOwner)
            {
                TempData[ErrorMessage] = "Only owners can delete the restaurant events!";
            }

            string? ownerId = await this.ownerService.OwnerIdByUserIdAsync(this.GetUserId()!);


            var @event = await this.eventService.GetEventByIdAsync(eventId);

            if (@event == null)
            {
                TempData[ErrorMessage] = "Event with the provided id does not exist!";
                return RedirectToAction("AllByRestaurant");
            }

            string eventRestaurantId = @event.RestaurantId.ToString();

            bool isOwnerOwnedRestaurant = await this.restaurantService
               .IsOwnerWithIdOwnedRestaurantWithIdAsync(eventRestaurantId, ownerId!);

            if (!isOwnerOwnedRestaurant)
            {
                this.TempData[ErrorMessage] = "You have to be owner of the restaurant to delete the events"!;

                return this.RedirectToAction("AllByRestaurant");
            }

            try
            {
                var eventForDelete = await this.eventService.GetEventByIdAsync(eventId);
                if (eventForDelete == null)
                {
                    return RedirectToAction("Index", "Home");
                }

                var restaurantId = eventForDelete.RestaurantId;

                await this.eventService.DeleteEventAsync(eventId);
                this.TempData[SuccessMessage] = "Event was deleted successfully!";
                return RedirectToAction("AllByRestaurant", "Event", new { restaurantId });
            }
            catch (Exception)
            {
                return Error();
            }

        }

        private IActionResult Error()
        {
            this.TempData[ErrorMessage] = "Unexpected error";
            return this.RedirectToAction("Index", "Home");
        }

        private string GetUserId()
           => User.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}
