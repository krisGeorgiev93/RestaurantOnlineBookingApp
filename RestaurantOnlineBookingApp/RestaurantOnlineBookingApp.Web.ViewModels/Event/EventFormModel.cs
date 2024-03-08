namespace RestaurantOnlineBookingApp.Web.ViewModels.Event
{
    using System.ComponentModel.DataAnnotations;
    using static RestaurantOnlineBookingApp.Common.ValidationConstants.Event;
    public class EventFormModel
    {
        public Guid Id { get; set; }

        [Required]
        [MinLength(TitleMinLength)]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "Event date is required")]
        [Display(Name = "Event Date")]
        [DataType(DataType.DateTime)]
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
