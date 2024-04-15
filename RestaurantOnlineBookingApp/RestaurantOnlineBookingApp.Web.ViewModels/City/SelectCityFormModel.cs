
namespace RestaurantOnlineBookingApp.Web.ViewModels.City
{
    using System.ComponentModel.DataAnnotations;
    using static Common.ValidationConstants.City;
    public class SelectCityFormModel
    {
        public int Id { get; set; }

        [MinLength(NameMinLength)]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;
    }
}
