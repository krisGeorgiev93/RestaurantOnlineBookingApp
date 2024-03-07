using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static RestaurantOnlineBookingApp.Common.ValidationConstants.Event;
namespace RestaurantOnlineBookingApp.Data.Models
{
    public class Event
    {
        public Event()
        {
            this.Id = Guid.NewGuid();
            this.Bookings = new List<Booking>();
            this.Meals = new List<Meal>();
        }
        public Guid Id { get; set; }
        [Required]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }
        public decimal Price {  get; set; }
        public string ImageUrl {  get; set; }
        public Guid RestaurantId { get; set; }

        [ForeignKey(nameof(RestaurantId))]
        [Required]
        public Restaurant Restaurant { get; set; }

        public ICollection<Meal> Meals { get; set; } 
        public ICollection<Booking> Bookings { get; set; } = null!;
    }
}

