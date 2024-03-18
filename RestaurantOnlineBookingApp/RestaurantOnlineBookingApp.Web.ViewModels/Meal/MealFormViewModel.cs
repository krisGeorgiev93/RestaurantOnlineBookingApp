using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using static RestaurantOnlineBookingApp.Common.ValidationConstants.Meal;
namespace RestaurantOnlineBookingApp.Web.ViewModels.Meal
{
    public class MealFormViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = "Please select an image.")]
        public IFormFile Image { get; set; } = null!;

        [Range(typeof(decimal), PriceMinValue, PriceMaxValue)]
        [Display(Name = "Meal Price")]
        public decimal Price { get; set; }

        [Required]
        public Guid RestaurantId { get; set; }
    }
}
