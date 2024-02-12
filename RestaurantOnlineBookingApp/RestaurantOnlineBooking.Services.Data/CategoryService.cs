
namespace RestaurantOnlineBooking.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using RestaurantOnlineBooking.Services.Data.Interfaces;
    using RestaurantOnlineBookingApp.Data;
    using RestaurantOnlineBookingApp.Web.ViewModels.Category;
    public class CategoryService : ICategoryService
    {
        private readonly RestaurantBookingDbContext dBContext;

        public CategoryService(RestaurantBookingDbContext dBContext)
        {
            this.dBContext = dBContext;
        }


        public async Task<IEnumerable<SelectCategoryFormModel>> GetAllCategoriesAsync()
        {
            var allCategories = await this.dBContext.Categories
                .AsNoTracking()
                .Select(c => new SelectCategoryFormModel
                {
                    Id = c.Id,
                    Name = c.Name,
                })
                .ToListAsync();

            return allCategories;

        }
    }
}
