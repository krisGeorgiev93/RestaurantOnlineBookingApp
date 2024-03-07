namespace RestaurantOnlineBookingApp.Web.ViewModels.Event
{
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

        public Guid RestaurantId { get; set; }
    }
}
