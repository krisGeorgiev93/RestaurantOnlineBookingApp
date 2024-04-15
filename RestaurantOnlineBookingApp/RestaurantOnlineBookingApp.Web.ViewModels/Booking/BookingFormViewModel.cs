namespace RestaurantOnlineBookingApp.Web.ViewModels.Booking
{
    using System.ComponentModel.DataAnnotations;
    using static Common.ValidationConstants.Booking;
    public class BookingFormViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Booking date is required")]
        [Display(Name = "Booking Date")]
        [DataType(DataType.Date)]
        public string BookingDate { get; set; }

        [Required]
        [Display(Name = "Reserved Time")]
        public string ReservedTime { get; set; }

        [Required(ErrorMessage = "Number of guests is required")]
        [Display(Name = "Number of Guests")]
        [Range(NumberOfGuestsMinValue, NumberOfGuestsMaxValue, ErrorMessage = "Number of guests must be greater than 0")]
        public int NumberOfGuests { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [MaxLength(FirstNameMaxLength, ErrorMessage = "First name must be max 30 characters long")]
        [MinLength(FirstNameMinLength, ErrorMessage = "First name must be at least 2 characters long")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "Last name is required")]
        [MaxLength(LastNameMaxLength, ErrorMessage = "Last name must be max 30 characters long")]
        [MinLength(LastNameMinLength, ErrorMessage = "Last name must be at least 2 characters long")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = null!;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [MaxLength(EmailMaxLength, ErrorMessage = "Email address must be max 30 characters long")]
        [MinLength(EmailMinLength, ErrorMessage = "Email address must be at least 5 characters long")]
        [Display(Name = "Email")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Invalid phone number")]
        [MaxLength(MaxPhoneLength, ErrorMessage = "Phone number must be max 14 characters long")]
        [MinLength(MinPhoneLength, ErrorMessage = "Phone number must be at least 10 characters long")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; } = null!;

        public Guid RestaurantId { get; set; }

        // Property to store the list of reserved time options for the select menu
        public List<string> ReservedTimeOptions { get; set; } = new List<string>();

    }
}
