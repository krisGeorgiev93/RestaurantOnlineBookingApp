using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantOnlineBooking.Services.Data;
using RestaurantOnlineBooking.Services.Data.Interfaces;
using RestaurantOnlineBookingApp.Data;
using RestaurantOnlineBookingApp.Web.ViewModels.Restaurant;
using System.Security.Claims;
using static RestaurantOnlineBookingApp.Common.NotificationMessages;

namespace RestaurantOnlineBookingApp.Web.Controllers
{
    [Authorize]
    public class RestaurantController : Controller
    {
        private readonly IOwnerService _ownerService;
        private readonly ICategoryService _categoryService;
        private readonly RestaurantBookingDbContext _dbContext;

        public RestaurantController(IOwnerService ownerService, ICategoryService categoryService, RestaurantBookingDbContext dbContext)
        {           
            _ownerService = ownerService;
            _categoryService = categoryService;
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            //only owners can add restaurants 
            bool isOwner = await this._ownerService.OwnerExistByIdAsync(this.User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            if (!isOwner)
            {
                this.TempData[ErrorMessage] = "You must become an owner to add new restaurants!";

                return RedirectToAction("Join", "Owner");
            }

            RestaurantFormModel model = new RestaurantFormModel()
            {
                Categories = await this._categoryService.GetAllCategoriesAsync()
            };

            return View(model);
        }
    }
}
