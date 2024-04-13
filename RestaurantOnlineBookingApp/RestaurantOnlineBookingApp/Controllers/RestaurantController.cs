namespace RestaurantOnlineBookingApp.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using RestaurantOnlineBooking.Services.Data.Interfaces;
    using RestaurantOnlineBooking.Services.Data.Models;
    using RestaurantOnlineBookingApp.Data;
    using RestaurantOnlineBookingApp.Data.Models;
    using RestaurantOnlineBookingApp.Infrastructure.Extensions;
    using RestaurantOnlineBookingApp.Web.ViewModels.Meal;
    using RestaurantOnlineBookingApp.Web.ViewModels.Restaurant;
    using System.Security.Claims;
    using static RestaurantOnlineBookingApp.Common.NotificationMessages;

    [Authorize]
    public class RestaurantController : Controller
    {
        private readonly IOwnerService _ownerService;
        private readonly ICategoryService _categoryService;
        private readonly IRestaurantService _restaurantService;
        private readonly ICityService _cityService;
        private readonly IMealService _mealService;
        private readonly IUserService _userService;
        private readonly IPhotoService _photoService;
        private readonly RestaurantBookingDbContext _dbContext;
        public RestaurantController(IOwnerService ownerService, ICategoryService categoryService,
            RestaurantBookingDbContext dbContext, IRestaurantService restaurantService, ICityService cityService, IMealService mealService, ICapacityService capacityService
            , IUserService userService, IPhotoService photoService)
        {
            _ownerService = ownerService;
            _categoryService = categoryService;
            _restaurantService = restaurantService;
            _cityService = cityService;
            _dbContext = dbContext;
            _mealService = mealService;
            _userService = userService;
            _photoService = photoService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> All([FromQuery] AllRestaurantsQueryModel queryModel)
        {
            AllRestaurantsFilteredServiceModel serviceModel =
                await this._restaurantService.AllAsync(queryModel);

            // Sorting the restaurants by average menu prices and average rating
            switch (queryModel.SortBy)
            {
                case SortOption.PriceAscending:
                case SortOption.PriceDescending:
                case SortOption.RatingAscending:
                case SortOption.RatingDescending:
                    serviceModel = await this._restaurantService.AllAsync(queryModel);
                    break;
                default:
                    break;
            }

            queryModel.Restaurants = serviceModel.Restaurants;
            queryModel.Cities = await _cityService.AllCitiesNamesAsync();
            queryModel.Categories = await _categoryService.AllCategoryNamesAsync();
            queryModel.TotalRestaurants = serviceModel.TotalRestaurantsCount;

            return View(queryModel);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            //only owners can add restaurants 
            bool isOwner = await this._ownerService.OwnerExistByIdAsync(this.User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            if (!isOwner)
            {
                this.TempData[ErrorMessage] = "You must join to owners club to add new restaurants!";

                return RedirectToAction("Join", "Owner");
            }

            try
            {
                RestaurantFormModel model = new RestaurantFormModel()
                {
                    Categories = await this._categoryService.GetAllCategoriesAsync(),
                    Cities = await this._cityService.GetAllCitiesAsync()
                };
                return View(model);
            }
            catch (Exception)
            {
                return this.Error();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(RestaurantFormModel model)
        {
            bool isOwner = await this._ownerService.OwnerExistByIdAsync(this.User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            if (!isOwner)
            {
                this.TempData[ErrorMessage] = "You must join to owners club to add new restaurants!";

                return RedirectToAction("Join", "Owner");
            }

            bool categoryExists = await this._categoryService.ExistByIdAsync(model.CategoryId);

            if (!categoryExists)
            {
                ModelState.AddModelError(nameof(model.CategoryId), "This category does not exist!");
            }

            if (!ModelState.IsValid)
            {
                model.Categories = await this._categoryService.GetAllCategoriesAsync();

                return this.View(model);
            }

            try
            {
                string? ownerId =
                    await _ownerService.OwnerIdByUserIdAsync(this.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
                string restaurantId =
                      await this._restaurantService.CreateAndReturnRestaurantIdAsync(model, ownerId!);

                this.TempData[SuccessMessage] = "Restaurant was added successfully!";
                return RedirectToAction("Details", "Restaurant", new { id = restaurantId });

            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty, "Unexpected error! Please try again later or contact administrator!");
                model.Categories = await this._categoryService.GetAllCategoriesAsync();

                return View(model);
            }

        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(string id)
        {
            bool restaurantExists = await this._restaurantService.RestaurantExistsByIdAsync(id);
            if (!restaurantExists)
            {
                this.TempData[ErrorMessage] = "Restaurant with this id does not exist!";
                return this.RedirectToAction("All", "Restaurant");
            }

            try
            {
                RestaurantDetailsViewModel model = await this._restaurantService
                .GetDetailsByIdAsync(id);

                model.OwnerInfo.FullName =
                    await this._userService.GetFullNameByEmailAsync(model.OwnerInfo.Email);

                return View(model);
            }
            catch (Exception)
            {
                return this.Error();
            }

        }

        [HttpGet]
        public async Task<IActionResult> Mine()
        {
            List<RestaurantAllViewModel> myRestaurants = new List<RestaurantAllViewModel>();

            string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            bool isUserOwner = await this._ownerService.OwnerExistByIdAsync(userId);

            try
            {
                if (isUserOwner)
                {
                    string? ownerId = await this._ownerService.OwnerIdByUserIdAsync(userId);

                    myRestaurants.AddRange(await this._restaurantService.AllByOwnerIdAsync(ownerId!));
                }
                else
                {
                    myRestaurants.AddRange(await this._restaurantService.AllByUserIdAsync(userId));
                }

                return View(myRestaurants);
            }
            catch (Exception)
            {
                return Error();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            bool restaurantExists = await this._restaurantService.RestaurantExistsByIdAsync(id);
            if (!restaurantExists)
            {
                this.TempData[ErrorMessage] = "Restaurant with this id does not exist!";
                return this.RedirectToAction("All", "Restaurant");
            }

            bool isUserOwner = await this._ownerService
                .OwnerExistByIdAsync(GetUserId()!);

            if (!isUserOwner && !this.User.IsAdmin())
            {
                this.TempData[ErrorMessage] = "Only owners can edit the restaurant information!";

                return this.RedirectToAction("All", "Restaurant");
            }

            string? ownerId = await this._ownerService.OwnerIdByUserIdAsync(this.GetUserId()!);

            bool isOwnerOwnedRestaurant = await this._restaurantService
                .IsOwnerWithIdOwnedRestaurantWithIdAsync(id, ownerId!);

            if (!isOwnerOwnedRestaurant && !this.User.IsAdmin())
            {
                this.TempData[ErrorMessage] = "You have to be owner of the restaurant you want to edit"!;

                return this.RedirectToAction("Mine", "Restaurant");
            }

            try
            {
                RestaurantFormModel model = await this._restaurantService
                .GetRestaurantForEditByIdAsync(id);

                model.Categories = await this._categoryService.GetAllCategoriesAsync();
                model.Cities = await this._cityService.GetAllCitiesAsync();

                return this.View(model);
            }
            catch (Exception)
            {
                this.TempData[ErrorMessage] = "Unexpected error";
                return this.RedirectToAction("Index", "Home");
            }

        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, RestaurantEditFormModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Cities = await this._cityService.GetAllCitiesAsync();
                model.Categories = await this._categoryService.GetAllCategoriesAsync();
                return this.View(model);
            }
            bool restaurantExists = await this._restaurantService.RestaurantExistsByIdAsync(id);
            if (!restaurantExists)
            {
                this.TempData[ErrorMessage] = "Restaurant with this id does not exist!";
                return this.RedirectToAction("All", "Restaurant");
            }

            bool isUserOwner = await this._ownerService
                .OwnerExistByIdAsync(GetUserId()!);

            if (!isUserOwner && !this.User.IsAdmin())
            {
                this.TempData[ErrorMessage] = "Only owners can edit the restaurant information!";

                return this.RedirectToAction("All", "Restaurant");
            }

            string? ownerId = await this._ownerService.OwnerIdByUserIdAsync(this.GetUserId()!);

            bool isOwnerOwnedRestaurant = await this._restaurantService
                .IsOwnerWithIdOwnedRestaurantWithIdAsync(id, ownerId!);

            if (!isOwnerOwnedRestaurant && !this.User.IsAdmin())
            {
                this.TempData[ErrorMessage] = "You have to be owner of the restaurant you want to edit"!;

                return this.RedirectToAction("Mine", "Restaurant");
            }

            try
            {               
                await this._restaurantService.EditRestaurantByIdAsync(id, model);
            }
            catch (Exception)
            {
                this.ModelState.AddModelError(string.Empty, "Unexpected error");
                model.Categories = await this._categoryService.GetAllCategoriesAsync();
                model.Cities = await this._cityService.GetAllCitiesAsync();

                return this.View(model);
            }

            this.TempData[SuccessMessage] = "Restaurant was edited successfully!";
            return this.RedirectToAction("Details", "Restaurant", new { id = id });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            bool restaurantExists = await this._restaurantService.RestaurantExistsByIdAsync(id);
            if (!restaurantExists)
            {
                this.TempData[ErrorMessage] = "Restaurant with this id does not exist!";
                return this.RedirectToAction("All", "Restaurant");
            }

            bool isUserOwner = await this._ownerService
                .OwnerExistByIdAsync(GetUserId()!);

            if (!isUserOwner && !this.User.IsAdmin())
            {
                this.TempData[ErrorMessage] = "Only owners can edit the restaurant information!";

                return this.RedirectToAction("Join", "Owner");
            }

            string? ownerId = await this._ownerService.OwnerIdByUserIdAsync(this.GetUserId()!);

            bool isOwnerOwnedRestaurant = await this._restaurantService
                .IsOwnerWithIdOwnedRestaurantWithIdAsync(id, ownerId!);

            if (!isOwnerOwnedRestaurant && !this.User.IsAdmin())
            {
                this.TempData[ErrorMessage] = "You have to be owner of the restaurant you want to edit"!;

                return this.RedirectToAction("Mine", "Restaurant");
            }

            try
            {
                RestaurantDeleteDetailsViewModel model =
                    await this._restaurantService.GetRestaurantForDeleteByIdAsync(id);

                return this.View(model);
            }
            catch (Exception)
            {
                return this.Error();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(RestaurantDeleteDetailsViewModel model, string id)
        {
            bool restaurantExists = await this._restaurantService.RestaurantExistsByIdAsync(id);
            if (!restaurantExists)
            {
                this.TempData[ErrorMessage] = "Restaurant with this id does not exist!";
                return this.RedirectToAction("All", "Restaurant");
            }

            bool isUserOwner = await this._ownerService
                .OwnerExistByIdAsync(GetUserId()!);

            if (!isUserOwner && !this.User.IsAdmin())
            {
                this.TempData[ErrorMessage] = "Only owners can edit the restaurant information!";

                return this.RedirectToAction("Join", "Owner");
            }

            string? ownerId = await this._ownerService.OwnerIdByUserIdAsync(this.GetUserId()!);

            bool isOwnerOwnedRestaurant = await this._restaurantService
                .IsOwnerWithIdOwnedRestaurantWithIdAsync(id, ownerId!);

            if (!isOwnerOwnedRestaurant && !this.User.IsAdmin())
            {
                this.TempData[ErrorMessage] = "You have to be owner of the restaurant you want to edit"!;

                return this.RedirectToAction("Mine", "Restaurant");
            }

            try
            {
                await this._restaurantService.DeleteRestaurantByIdAsync(id);

                this.TempData[WarningMessage] = "The restaurant was successfully deleted!";
                return this.RedirectToAction("Mine", "Restaurant");
            }
            catch (Exception)
            {
                return this.Error();
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Menu(string restaurantId)
        {
            var restaurant = await _restaurantService.GetRestaurantByIdAsync(restaurantId);
            if (restaurant == null)
            {
                return NotFound();
            }

            try
            {
                var meals = await _mealService.GetAllMealsForRestaurantByIdAsync(restaurantId);
                // Check if the current user is the owner of the restaurant
                bool isOwner = await _ownerService.HasRestaurantWithIdAsync(User.GetId(), restaurantId);
                var mealViewModels = meals.Select(m => new MealAllViewModel
                {
                    Id = m.Id.ToString(),
                    Name = m.Name,
                    Description = m.Description,
                    ImageUrl = m.ImageUrl,
                    Price = m.Price,
                });
                ViewBag.RestaurantId = restaurantId;
                return View((mealViewModels, isOwner));
            }
            catch (Exception)
            {
                return this.Error();
            }

        }

        [HttpPost]
        public async Task<IActionResult> AddRestaurantToFavorites(Guid restaurantId)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (!Guid.TryParse(userId, out Guid parsedUserId))
                {
                    return BadRequest("Invalid user ID format.");
                }

                // Извикване на сървисния метод за добавяне на ресторант към списъка с любими
                await this._restaurantService.AddRestaurantToFavoriteAsync(userId, restaurantId);

                return RedirectToAction("Favorites", "Restaurant");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while adding restaurant to favorites: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Favorites()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var favoriteRestaurantsHashSet = await this._restaurantService.GetFavoriteRestaurantsAsync(userId);

            var favoriteRestaurantsList = favoriteRestaurantsHashSet.ToList();
            var favoriteRestaurantsViewModels = favoriteRestaurantsList.Select(r => new RestaurantAllViewModel
            {
                Id = r.Id.ToString(),
                Name = r.Name,
                Description = r.Description,
                ImageUrl = r.ImageUrl,
            });            
            return View(favoriteRestaurantsViewModels);
        }

        [HttpPost]
        public async Task<IActionResult> UploadPhotos(List<IFormFile> photoFiles, string restaurantId)
        {
            if (photoFiles == null || !photoFiles.Any())
            {
                ModelState.AddModelError("photoFiles", "Please select at least one photo to upload.");
                return View("Restaurant", restaurantId); 
            }

            var restaurant = await _restaurantService.GetRestaurantByIdAsync(restaurantId);
            if (restaurant == null)
            {
                return NotFound();
            }
           

            bool isUserOwner = await this._ownerService
                .OwnerExistByIdAsync(GetUserId()!);

            if (!isUserOwner && !this.User.IsAdmin())
            {
                this.TempData[ErrorMessage] = "Only the owner can edit the restaurant information!";

                return this.RedirectToAction("All", "Restaurant");
            }

            string? ownerId = await this._ownerService.OwnerIdByUserIdAsync(this.GetUserId()!);

            bool isOwnerOwnedRestaurant = await this._restaurantService
                .IsOwnerWithIdOwnedRestaurantWithIdAsync(restaurantId, ownerId!);

            if (!isOwnerOwnedRestaurant && !this.User.IsAdmin())
            {
                this.TempData[ErrorMessage] = "You have to be owner of the restaurant you want to upload photos"!;

                return this.RedirectToAction("Mine", "Restaurant");
            }
            try
            {
                foreach (var photoFile in photoFiles)
                {
                    var uploadResult = await _photoService.AddPhotoAsync(photoFile);
                    var photo = new Photo { Url = uploadResult.SecureUrl.ToString(), RestaurantId = Guid.Parse(restaurantId) };
                    await _restaurantService.AddPhotoToRestaurantAsync(restaurantId.ToString(), photo);
                }
                this.TempData[SuccessMessage] = "Photos uploaded successfully!";
                return RedirectToAction("AllPhotos", "Restaurant", new { restaurantId });
            }
            catch (Exception)
            {
                this.TempData[ErrorMessage] = "Unexpected error";
                return this.RedirectToAction("Index", "Home");
            }           

        }

        [HttpPost]
        public async Task<IActionResult> ChangeMainPhoto(IFormFile newMainPhoto, string restaurantId)
        {
            // Проверка за наличие на избрана снимка
            if (newMainPhoto == null || newMainPhoto.Length == 0)
            {
                ModelState.AddModelError("newMainPhoto", "Please select a photo to set as the main photo.");
                return RedirectToAction("Details", "Restaurant", new { id = restaurantId }); 
            }

            // Проверка за съществуване на ресторанта
            var restaurant = await _restaurantService.GetRestaurantByIdAsync(restaurantId);
            if (restaurant == null)
            {
                return NotFound();
            }

            // Проверка дали потребителят е собственик или администратор
            //bool isUserOwner = await this._ownerService
            //   .OwnerExistByIdAsync(GetUserId()!);           

            string? ownerId = await this._ownerService.OwnerIdByUserIdAsync(this.GetUserId()!);

            bool isOwnerOwnedRestaurant = await this._restaurantService
                .IsOwnerWithIdOwnedRestaurantWithIdAsync(restaurantId, ownerId!);
            if (!isOwnerOwnedRestaurant && !User.IsAdmin())
            {
                TempData["ErrorMessage"] = "Only the owner and admin can change the main photo!";
                return RedirectToAction("All", "Restaurant");
            }

            // Промяна на главната снимка на ресторанта
            try
            {
                var uploadResult = await _photoService.AddPhotoAsync(newMainPhoto);

                // Обновяване на снимката на ресторанта в базата данни
                restaurant.ImageUrl = uploadResult.Url.ToString();
                this._dbContext.Entry(restaurant).Property(x => x.ImageUrl).IsModified = true;
                await this._dbContext.SaveChangesAsync();

                TempData["SuccessMessage"] = "Main photo changed successfully!";
                return RedirectToAction("Details", "Restaurant", new { id = restaurantId });
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Unexpected error";
                return RedirectToAction("Index", "Home");
            }
        }

        [AllowAnonymous]
        public async Task<IActionResult> AllPhotos(string restaurantId)
        {
            var photos = await _restaurantService.GetRestaurantPhotosAsync(restaurantId);
            ViewBag.RestaurantId = restaurantId; // Предаваме restaurantId през ViewBag
            return View(photos);
        }

        [HttpPost]
        public async Task<IActionResult> DeletePhoto(string photoId, string restaurantId)
        {
            try
            {
                var deletionResult = await _photoService.DeletePhotoAsync(photoId);

                await _restaurantService.DeletePhotoAsync(photoId);

                TempData[SuccessMessage] = "Image was successfully deleted!";
                return RedirectToAction("AllPhotos", "Restaurant", new { restaurantId });
            }
            catch (Exception)
            {
                return BadRequest("Failed to delete photo.");
            }
        }
        private IActionResult Error()
        {
            this.TempData[ErrorMessage] = "Unexpected error";
            return this.RedirectToAction("Index", "Home");
        }

        // Get currently logged-in user's Id
        private string GetUserId()
           => User.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}
