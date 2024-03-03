using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantOnlineBookingApp.Web.ViewModels.Booking
{
    public class BookingFormViewModel
    {
        [Required(ErrorMessage = "Booking date is required")]
        [Display(Name = "Booking Date")]
        public DateTime BookingDate { get; set; }

        [Required(ErrorMessage = "Reserved time is required")]
        [Display(Name = "Reserved Time")]
        public TimeSpan ReservedTime { get; set; }

        [Required(ErrorMessage = "Number of guests is required")]
        [Display(Name = "Number of Guests")]
        [Range(1, int.MaxValue, ErrorMessage = "Number of guests must be greater than 0")]
        public int NumberOfGuests { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "Last name is required")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = null!;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [Display(Name = "Email")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Invalid phone number")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; } = null!;

        public Guid RestaurantId { get; set; }

    }
}
