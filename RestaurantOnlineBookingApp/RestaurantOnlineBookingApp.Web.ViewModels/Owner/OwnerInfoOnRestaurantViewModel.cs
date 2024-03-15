using System.ComponentModel.DataAnnotations;

namespace RestaurantOnlineBookingApp.Web.ViewModels.Owner
{
    public class OwnerInfoOnRestaurantViewModel
    {
        [Display(Name = "Full Name")]
        public string FullName { get; set; } = null!;

        [Display(Name = "Email Address")]
        public string Email { get; set; } = null!;

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; } = null!;
    }
}
