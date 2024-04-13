namespace RestaurantOnlineBookingApp.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using RestaurantOnlineBooking.Services.Data.Interfaces;
    using RestaurantOnlineBookingApp.Infrastructure.Extensions;
    using RestaurantOnlineBookingApp.Web.ViewModels.Review;
    using System.Security.Claims;
    using static RestaurantOnlineBookingApp.Common.NotificationMessages;

    [Authorize]
    public class ReviewController : Controller
    {
        private readonly IReviewService _reviewService;
        private readonly IBookingService _bookingService;
        private readonly IOwnerService _ownerService;

        public ReviewController(IReviewService reviewService, IBookingService bookingService,
            IOwnerService ownerService)
        {
            _reviewService = reviewService;
            _bookingService = bookingService;
            _ownerService = ownerService;
        }

        [HttpGet]
        public async Task<IActionResult> AddReview(Guid restaurantId)
        {
            var userId = User.GetId();

            if (!Guid.TryParse(userId, out Guid guestId))
            {
                return this.Error();
            }

            // Проверка дали потребителят е собственик на ресторанта
            bool isOwner = await this._ownerService.HasRestaurantWithIdAsync(userId, restaurantId.ToString());

            if (isOwner)
            {
                this.TempData[ErrorMessage] = "Owners are not allowed to leave reviews for their own restaurants.";
                return RedirectToAction("Mine", "Restaurant");
            }

            // Проверка за наличие на валидна резервация с минала дата
            bool hasValidReservation = await _bookingService.HasValidReservationAsync(restaurantId, guestId);

            // Проверка дали гостът има валидна резервация с минала дата
            if (!hasValidReservation)
            {
                // Ако няма валидна резервация с минала дата, върнете грешка
                this.TempData[ErrorMessage] = "You must have a valid reservation with a past date to leave a review.";
                return RedirectToAction("Mine", "Booking");
            }


            // Извличане на потребителския имейл от аутентикационния сервиз
            var userEmail = User.Identity.Name;

            // Извличане на първото и последното име на потребителя от клеймовете
            var userFirstName = User.FindFirstValue(ClaimTypes.GivenName);
            var userLastName = User.FindFirstValue(ClaimTypes.Surname);

            // Създаване на модела
            var model = new AddReviewViewModel
            {
                GuestId = guestId,
                RestaurantId = restaurantId,
                GuestEmail = userEmail,
                FirstName = userFirstName, 
                LastName = userLastName 
            };

            // Връщате изгледа, който показва формата за добавяне на ревюто, като предоставяте модела като данни за изгледа.
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddReview(AddReviewViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                // Set the GuestId based on the current user
                var userId = Guid.Parse(User.GetId());
                model.GuestId = userId;

                await _reviewService.AddReviewAsync(model);

                return RedirectToAction("Details", "Restaurant", new { id = model.RestaurantId });
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "An error occurred while processing your request.");
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> MyReviews()
        {
            var userId = User.GetId();
            var reviews = await _reviewService.GetReviewsByUserIdAsync(userId);
            return View(reviews);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> All(Guid restaurantId)
        {
            var reviews = await _reviewService.GetReviewsForRestaurantAsync(restaurantId);
            var model = new ReviewViewModel
            {
                Reviews = reviews,
                SortBy = SortOption.RatingDescending // Default sorting option
            };
            ViewBag.RestaurantId = restaurantId;
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> SortReviews(Guid restaurantId, SortOption sortBy)
        {
            var sortedReviews = await _reviewService.GetSortedReviewsAsync(restaurantId, sortBy);
            var model = new ReviewViewModel
            {
                Reviews = sortedReviews,
                SortBy = sortBy
            };
            ViewBag.RestaurantId = restaurantId;
            return View("All", model);
        }
                     
       
        private IActionResult Error()
        {
            this.TempData[ErrorMessage] = "Unexpected error";
            return this.RedirectToAction("Index", "Home");
        }
    }
}
