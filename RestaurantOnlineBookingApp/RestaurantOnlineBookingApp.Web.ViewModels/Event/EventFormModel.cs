namespace RestaurantOnlineBookingApp.Web.ViewModels.Event
{
    using RestaurantOnlineBookingApp.Web.ViewModels.Meal;
    using System.ComponentModel.DataAnnotations;
    using static RestaurantOnlineBookingApp.Common.ValidationConstants.Event;
    public class EventFormModel
    {
        [Required]
        [MinLength(TitleMinLength)]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required]
        [MinLength(DescriptionMinLength)]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [Required]
        [Range(typeof(decimal),MinPriceValue,MaxPriceValue)]
        public decimal Price { get; set; }
        public string ImageUrl { get; set; } = null!;

        // Списък с ID на ястия, които ще бъдат добавени към събитието
        public List<int> SelectedMealIds { get; set; }

        // Списък с налични ястия за избор
        public IEnumerable<MealFormViewModel> AvailableMeals { get; set; }
        public Guid RestaurantId { get; set; }
    }
}
