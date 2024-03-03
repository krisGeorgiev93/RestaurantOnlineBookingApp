using Microsoft.AspNetCore.Mvc;
using RestaurantOnlineBooking.Services.Data.Interfaces;
using RestaurantOnlineBookingApp.Web.ViewModels.Booking;
using RestaurantOnlineBookingApp.Web.ViewModels.Meal;
using System.Globalization;
using System.Security.Claims;

namespace RestaurantOnlineBookingApp.Web.Controllers
{
    public class BookingController : Controller
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet]
        public IActionResult BookTable(string restaurantId)
        {
            if (!Guid.TryParse(restaurantId, out Guid restaurantGuid))
            {
                return BadRequest("Invalid restaurantId");
            }
            var viewModel = new BookingFormViewModel
            {
                RestaurantId = restaurantGuid
            };

            return View(viewModel);
           
        }

        [HttpPost]
        public async Task<IActionResult> BookTable(BookingFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            
          
            var bookingDate = DateTime.Now.Date; 
            var restaurantId = model.RestaurantId.ToString();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _bookingService.BookTableAsync(restaurantId,model,userId);

            if (result)
            {
                return RedirectToAction("Mine", "Booking"); 
            }

            else
            {
                ModelState.AddModelError(string.Empty, "Failed to book the table. Please try again.");
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Mine()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var bookings = await _bookingService.GetBookingsByUserIdAsync(userId);

            var model = bookings.Select(b => new BookingAllViewModel
            {
                BookingDate = b.BookingDate,
                ReservedTime = b.ReservedTime,
                NumberOfGuests = b.NumberOfGuests,
                RestaurantId = b.RestaurantId,
                RestaurantName = b.RestaurantName,
                ImageUrl = b.ImageUrl,
            }).ToList();

            return View(model);
        }
    }
}
