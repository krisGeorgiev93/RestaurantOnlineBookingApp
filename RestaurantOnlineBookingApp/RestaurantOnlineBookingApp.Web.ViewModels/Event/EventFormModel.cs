namespace RestaurantOnlineBookingApp.Web.ViewModels.Event
{
    using Microsoft.AspNetCore.Http;
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
        public DateTime Date { get; set; }

        [Required]
        public TimeSpan Time { get; set; }

        [Required]
        [MinLength(DescriptionMinLength)]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [Required]
        [Range(typeof(decimal),MinPriceValue,MaxPriceValue)]
        public decimal Price { get; set; }

        [Required]  
        public IFormFile Image { get; set; } = null!;
       
        public Guid RestaurantId { get; set; }
    }
}
