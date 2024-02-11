

namespace RestaurantOnlineBookingApp.Web.ViewModels.Owner
{
    using System.ComponentModel.DataAnnotations;
    using static Common.ValidationConstants.Owner;
    public class JoinOwnerFormModel
    {
        [Required]
        [StringLength(PhoneNumberMaxLength, MinimumLength = PhoneNumberMinLength)]
        [Phone]
        [Display(Name = "Phone")]
        public string PhoneNumber { get; set; } = null!;
    }
}
