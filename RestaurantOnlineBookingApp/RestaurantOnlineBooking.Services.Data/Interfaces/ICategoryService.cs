using RestaurantOnlineBookingApp.Web.ViewModels.Category;

namespace RestaurantOnlineBooking.Services.Data.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<SelectCategoryFormModel>> GetAllCategoriesAsync();

        Task<bool>ExistByIdAsync(int id);

        Task<IEnumerable<string>> AllCategoryNamesAsync();
    }
}
