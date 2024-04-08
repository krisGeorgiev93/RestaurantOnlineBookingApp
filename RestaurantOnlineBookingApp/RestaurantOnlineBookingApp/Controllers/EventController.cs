
namespace RestaurantOnlineBookingApp.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using RestaurantOnlineBooking.Services.Data.Interfaces;
    using RestaurantOnlineBookingApp.Infrastructure.Extensions;
    using RestaurantOnlineBookingApp.Web.ViewModels.Event;
    using System.Security.Claims;
    using static RestaurantOnlineBookingApp.Common.NotificationMessages;

    [Authorize]
    public class EventController : Controller
    {
        private readonly IEventService eventService;
        private readonly IOwnerService ownerService;
        private readonly IRestaurantService restaurantService;
        public EventController(IEventService eventService, IOwnerService ownerService, 
            IRestaurantService restaurantService)
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

            if (!isOwner && !User.IsAdmin())
            {
                this.TempData[ErrorMessage] = "You must become an owner to add new restaurants!";

                return RedirectToAction("All", "Restaurant");
            }

            string? ownerId = await this.ownerService.OwnerIdByUserIdAsync(this.User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            bool isOwnerOwnedRestaurant = await this.restaurantService
                .IsOwnerWithIdOwnedRestaurantWithIdAsync(restaurantId, ownerId!);

            if (!isOwnerOwnedRestaurant && !User.IsAdmin())
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
                this.TempData[SuccessMessage] = "Event was created successfully!";
                return RedirectToAction("AllByRestaurant", "Event", new { restaurantId });                
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
            bool eventExists = await this.eventService.EventExistsByIdAsync(id);

            if (!eventExists)
            {
                TempData[ErrorMessage] = "Event with the provided id does not exist!";

                return RedirectToAction("Index", "Home");
            }

            bool isUserOwner = await this.ownerService.OwnerExistByIdAsync(GetUserId()!);

            if (!isUserOwner && !User.IsAdmin())
            {
                TempData[ErrorMessage] = "Only owners can edit the restaurant events!";
            }

            string? ownerId = await this.ownerService.OwnerIdByUserIdAsync(this.GetUserId()!);


            var @event = await this.eventService.GetEventByIdAsync(id);

            if (@event == null)
            {
                TempData[ErrorMessage] = "Event with the provided id does not exist!";
                return RedirectToAction("Index", "Home");
            }

            string eventRestaurantId = @event.RestaurantId.ToString();

            bool isOwnerOwnedRestaurant = await this.restaurantService
               .IsOwnerWithIdOwnedRestaurantWithIdAsync(eventRestaurantId, ownerId!);

            if (!isOwnerOwnedRestaurant && !User.IsAdmin())
            {
                this.TempData[ErrorMessage] = "You have to be owner of the restaurant to edit the events"!;

                return RedirectToAction("Index", "Home");
            }

            var eventForEdit = await this.eventService.GetEventByIdAsync(id);
            if (eventForEdit == null)
            {
                return NotFound("Event not found");
            }

            try
            {
                //var photo = await this.photoService.AddPhotoAsync(eventForEdit.Image);

                var eventFormModel = new EventFormModel
                {
                    Id = eventForEdit.Id,
                    Title = eventForEdit.Title,
                    Date = eventForEdit.Date,
                    Time = eventForEdit.Time,
                    Description = eventForEdit.Description,
                    Price = eventForEdit.Price,
                    Image = eventForEdit.Image
                };
                return View(eventFormModel);
            }
            catch (Exception)
            {
                this.TempData[ErrorMessage] = "Unexpected error";
                return this.RedirectToAction("Index", "Home");
            }

        }

        [HttpPost]
        public async Task<IActionResult> Edit(EventFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            bool eventExists = await this.eventService.EventExistsByIdAsync(model.Id.ToString());

            if (!eventExists)
            {
                TempData[ErrorMessage] = "Event with the provided id does not exist!";

                return RedirectToAction("Index", "Home");
            }

            bool isUserOwner = await this.ownerService.OwnerExistByIdAsync(GetUserId()!);

            if (!isUserOwner && !User.IsAdmin())
            {
                TempData[ErrorMessage] = "Only owners can edit the restaurant events!";
            }

            string? ownerId = await this.ownerService.OwnerIdByUserIdAsync(this.GetUserId()!);


            var @event = await this.eventService.GetEventByIdAsync(model.Id.ToString());

            if (@event == null)
            {
                TempData[ErrorMessage] = "Event with the provided id does not exist!";
                return RedirectToAction("Index", "Home");
            }

            string eventRestaurantId = @event.RestaurantId.ToString();

            bool isOwnerOwnedRestaurant = await this.restaurantService
               .IsOwnerWithIdOwnedRestaurantWithIdAsync(eventRestaurantId, ownerId!);

            if (!isOwnerOwnedRestaurant && !User.IsAdmin())
            {
                this.TempData[ErrorMessage] = "You have to be owner of the restaurant to edit the events"!;

                return RedirectToAction("Index", "Home");
            }
            try
            {
                await this.eventService.EditEventAsync(model);
                this.TempData[SuccessMessage] = "Event was edited successfully!";
                return RedirectToAction("AllByRestaurant", "Event", new { restaurantId = eventRestaurantId });

            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("Unexpected error", ex.Message);
                return View(model);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> AllByRestaurant(string restaurantId)
        {
            var events = await this.eventService.GetAllEventsByRestaurantIdAsync(restaurantId);
            var eventViewModels = events.Select(e => new EventViewModel
            {
                Id = e.Id.ToString(),
                Title = e.Title,
                Date = e.Date,
                Time = e.Time,
                Description = e.Description,
                Price = e.Price,
                ImageUrl = e.ImageUrl
            }).ToList();
            ViewBag.RestaurantId = restaurantId;
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

            if (!isUserOwner && !User.IsAdmin())
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

            if (!isOwnerOwnedRestaurant && !User.IsAdmin())
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
