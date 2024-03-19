

namespace RestaurantOnlineBookingApp.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using RestaurantOnlineBooking.Services.Data.Interfaces;
    using RestaurantOnlineBookingApp.Data;
    using RestaurantOnlineBookingApp.Web.ViewModels.Booking;
    using System.Security.Claims;
    using static RestaurantOnlineBookingApp.Common.NotificationMessages;
    public class BookingController : Controller
    {
        private readonly IBookingService _bookingService;
        private readonly IRestaurantService _restaurantService;
        private readonly RestaurantBookingDbContext _dBContext;
        public BookingController(IBookingService bookingService, RestaurantBookingDbContext dBContext, IRestaurantService restaurantService)
        {
            _bookingService = bookingService;
            _dBContext = dBContext;
            _restaurantService = restaurantService;
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

            // Retrieve the restaurant from the database
            var restaurant = await this._dBContext.Restaurants.FirstOrDefaultAsync(r => r.Id.ToString() == restaurantId);
            if (restaurant == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid restaurant ID");
                return View(model);
            }

            // Get the starting and ending times of the restaurant
            var startingTime = restaurant.StartingTime;
            var endingTime = restaurant.EndingTime;

            var bookingTime = model.ReservedTime;           

            // Check if the requested number of guests exceeds the restaurant's capacity
            if (model.NumberOfGuests > restaurant.Capacity)
            {
               TempData[ErrorMessage] = "Number of guests exceeds restaurant capacity";
                return View(model);
               //return RedirectToAction("All", "Restaurant");
            }
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
                Id = b.Id,
                BookingDate = b.BookingDate,
                ReservedTime = b.ReservedTime,
                NumberOfGuests = b.NumberOfGuests,
                RestaurantId = b.RestaurantId,
                RestaurantName = b.RestaurantName,
                ImageUrl = b.ImageUrl,
            }).ToList();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            bool bookingExists = await this._bookingService.BookingExistsByIdAsync(id);

            if (!bookingExists)
            {
                TempData[ErrorMessage] = "Booking with the provided id does not exist!";

                return RedirectToAction("Mine", "Booking");
            }

            var booking = await this._bookingService.GetBookingByIdAsync(id);

            if (booking == null)
            {
                TempData[ErrorMessage] = "Booking with the provided id does not exist!";
                return RedirectToAction("Mine", "Booking");
            }
                        
            try
            {
                var bookingForDelete = await this._bookingService.GetBookingByIdAsync(id);

                if (bookingForDelete == null)
                {
                    return RedirectToAction("Index", "Home");
                }

                await this._bookingService.DeleteBookingAsync(id);
                TempData["SuccessMessage"] = "Booking was deleted successfully!";
                return RedirectToAction("Mine", "Booking");
            }
            catch (Exception)
            {
                return this.Error();
            }
        }

        public IActionResult GetReservedTimes(Guid restaurantId)
        {
            // Get the restaurant's operating hours from the database
            var restaurant = this._restaurantService.GetRestaurantByIdAsync(restaurantId.ToString()).Result;

            // If the restaurant is not found or its operating hours are not set, return an empty list
            if (restaurant == null || restaurant.StartingTime == null || restaurant.EndingTime == null)
            {
                return Json(new List<string>());
            }

            // Calculate the reserved times based on the restaurant's operating hours
            TimeSpan startingTime = restaurant.StartingTime;
            TimeSpan endingTime = restaurant.EndingTime;
            TimeSpan interval = TimeSpan.FromHours(1); // Adjust the interval as needed
            var reservedTimes = GetReservedTimes(startingTime, endingTime, interval);

            return Json(reservedTimes);
        }

        // Helper method to calculate reserved times
        private List<string> GetReservedTimes(TimeSpan startingTime, TimeSpan endingTime, TimeSpan interval)
        {
            List<string> reservedTimes = new List<string>();

            for (TimeSpan time = startingTime; time < endingTime; time = time.Add(interval))
            {
                reservedTimes.Add(time.ToString(@"hh\:mm"));
            }

            return reservedTimes;
        }

        private IActionResult Error()
        {
            this.TempData[ErrorMessage] = "Unexpected error";
            return this.RedirectToAction("Index", "Home");
        }
    }
}
