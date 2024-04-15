namespace RestaurantOnlineBookingApp.Web.ViewModels.Category
{
    using System.ComponentModel.DataAnnotations;
    using static Common.ValidationConstants.Category;
    public class SelectCategoryFormModel
    {        
        public int Id { get; set; }

        [MinLength(NameMinLength)]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;


    }
}
