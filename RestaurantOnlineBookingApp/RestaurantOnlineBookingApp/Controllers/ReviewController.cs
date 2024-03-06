using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RestaurantOnlineBooking.Services.Data.Interfaces;
using RestaurantOnlineBookingApp.Data.Models;
using RestaurantOnlineBookingApp.Web.ViewModels.Review;
using System.Security.Claims;

namespace RestaurantOnlineBookingApp.Web.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet]
        public IActionResult AddReview(Guid restaurantId)
        {
            // Предполагайки, че имате потребителска идентификация, извличате ID на потребителя от текущият потребителски клейм, за да го използвате като GuestId в модела за добавяне на ревюто.
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Проверявате дали е възможно да извлечете валидно идентификатора на потребителя.
            if (!Guid.TryParse(userId, out Guid guestId))
            {
                // Ако не е възможно, да се хвърли изключение или да се върне подходящ резултат, който показва, че не можете да идентифицирате потребителя.
                // Пример: throw new ApplicationException("Unable to identify the user.");
                return BadRequest("Unable to identify the user.");
            }

            // Създавате модела за добавяне на ревюто, като задавате идентификатора на потребителя като GuestId и идентификатора на ресторанта.
            var model = new AddReviewViewModel
            {
                GuestId = guestId,
                RestaurantId = restaurantId
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
                var userId = Guid.Parse(GetUserId());
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
            var userId = GetUserId();
            var reviews = await _reviewService.GetReviewsByUserIdAsync(userId);
            return View(reviews);
        }

        public async Task<IActionResult> All(Guid restaurantId)
        {
            var reviews = await _reviewService.GetReviewsForRestaurantAsync(restaurantId);
            return View(reviews);
        }

        private string GetUserId()
           => User.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}
