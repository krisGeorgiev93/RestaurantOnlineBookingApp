using RestaurantOnlineBookingApp.Web.ViewModels.Category;

namespace RestaurantOnlineBooking.Services.Data.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<SelectCategoryFormModel>> GetAllCategoriesAsync();

        Task<bool>ExistByIdAsync(int id);

        Task<bool> ExistByNameAsync(string categoryName);


        Task<IEnumerable<string>> AllCategoryNamesAsync();
    }
}
