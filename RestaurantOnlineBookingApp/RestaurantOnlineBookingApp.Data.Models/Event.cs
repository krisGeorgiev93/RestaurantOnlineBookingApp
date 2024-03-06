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
        public int MealId { get; set; }

        [ForeignKey(nameof(MealId))]
        [Required]
        public Meal Meal { get; set; }
        public ICollection<Booking> Bookings { get; set; } = null!;
    }
}

