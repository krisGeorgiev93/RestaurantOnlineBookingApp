namespace RestaurantOnlineBooking.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using RestaurantOnlineBooking.Services.Data.Interfaces;
    using RestaurantOnlineBookingApp.Data;
    using RestaurantOnlineBookingApp.Data.Models;
    using RestaurantOnlineBookingApp.Web.ViewModels.Meal;

    public class MealService : IMealService
    {
        private readonly RestaurantBookingDbContext dBContext;
        private readonly IPhotoService photoService;

        public MealService(RestaurantBookingDbContext dBContext, IPhotoService photoService)
        {
            this.dBContext = dBContext;
            this.photoService = photoService;

        }

        public async Task AddMealToRestaurantAsync(string restaurantId, MealFormViewModel mealViewModel)
        {
            if (!Guid.TryParse(restaurantId, out Guid restaurantGuid))
            {
                throw new ArgumentException("Invalid restaurantId");
            }

            var restaurant = await this.dBContext.Restaurants.FindAsync(restaurantGuid);

            if (restaurant == null)
            {
                throw new InvalidOperationException("Restaurant not found.");
            }
            var photo = await this.photoService.AddPhotoAsync(mealViewModel.Image);
            var meal = new Meal
            {
                Name = mealViewModel.Name,
                Description = mealViewModel.Description,
                Price = mealViewModel.Price,
                ImageUrl = photo.Url.ToString(),
                RestaurantId = restaurantGuid // Associate the meal with the restaurant
            };

            restaurant.Meals.Add(meal);
            await dBContext.SaveChangesAsync();
        }


        public async Task CreateAsync(MealFormViewModel mealFormViewModel)
        {
            var photo = await this.photoService.AddPhotoAsync(mealFormViewModel.Image);
            Meal meal = new Meal()
            {
                Name = mealFormViewModel.Name,
                Description = mealFormViewModel.Description,
                Price = mealFormViewModel.Price,
                ImageUrl = photo.Url.ToString(),
            };

            await this.dBContext.Meals.AddAsync(meal);
            await this.dBContext.SaveChangesAsync();
        }

        public async Task DeleteMealAsync(string mealId)
        {
            var meal = await this.dBContext
                  .Meals.FirstAsync(m => m.Id.ToString() == mealId);

            if (meal == null)
            {
                throw new InvalidOperationException("Meal not found.");
            }

            this.dBContext.Meals.Remove(meal);

            await this.dBContext.SaveChangesAsync();
        }

        public async Task EditMealAsync(MealFormViewModel model)
        {
            var meal = await this.dBContext.Meals.FindAsync(model.Id);

            if (meal == null)
            {
                throw new InvalidOperationException("Meal not found.");
            }

            var photo = await this.photoService.AddPhotoAsync(model.Image);

            meal.Name = model.Name;
            meal.Description = model.Description;
            meal.Price = model.Price;
            meal.ImageUrl = photo.Url.ToString();
            meal.RestaurantId = model.RestaurantId;

            await this.dBContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<MealAllViewModel>> GetAllMealsAsync()
        {
            var meals = await this.dBContext
                 .Meals
                .Select(m => new MealAllViewModel()
                {
                    Id = m.Id.ToString(),
                    Name = m.Name,
                    Description = m.Description,
                    Price = m.Price,
                    ImageUrl = m.ImageUrl,
                })
                .ToListAsync();

            return meals;
        }

        public async Task<IEnumerable<Meal>> GetAllMealsForRestaurantByIdAsync(string restaurantId)
        {
            return await this.dBContext
                .Meals
                .Where(m => m.RestaurantId.ToString() == restaurantId)
                .ToListAsync();
        }

        public async Task<MealFormViewModel> GetMealByIdAsync(string mealId)
        {
            var meal = await this.dBContext.Meals.FirstAsync(m => m.Id.ToString() == mealId);

            if (meal == null)
            {
                throw new InvalidOperationException("Meal not found.");
            }

            var mealForm = new MealFormViewModel()
            {
                Id = meal.Id,
                Name = meal.Name,
                Description = meal.Description,
                Price = meal.Price,
                RestaurantId = (Guid)meal.RestaurantId
            };

            return mealForm;

        }

        public async Task<bool> MealExistsByIdAsync(string mealId)
        {
            bool IsExists = await this.dBContext
                .Meals
                .AnyAsync(m => m.Id.ToString() == mealId);

            return IsExists;
        }
    }
}
