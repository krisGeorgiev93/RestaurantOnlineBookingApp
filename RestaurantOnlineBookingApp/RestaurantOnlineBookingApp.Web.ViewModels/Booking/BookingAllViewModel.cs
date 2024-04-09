
namespace RestaurantOnlineBookingApp.Web.ViewModels.Booking
{
    using System.ComponentModel.DataAnnotations;
    public class BookingAllViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Booking Date")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public string BookingDate { get; set; }
        public string GuestName { get; set; } 
        public string GuestEmail { get; set; }
        public string ReservedTime { get; set; }
        public int NumberOfGuests { get; set; }
        public string RestaurantName { get; set; }

        public string ImageUrl { get; set; }
        public Guid RestaurantId { get; set; }
    }
}
