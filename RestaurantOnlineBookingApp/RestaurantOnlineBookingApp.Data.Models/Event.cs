using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public string Title { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string Description { get; set; }
        public int MealId { get; set; }

        [ForeignKey(nameof(MealId))]
        [Required]
        public Meal Meal { get; set; }
        public ICollection<Booking> Bookings { get; set; } = null!;
    }
}

