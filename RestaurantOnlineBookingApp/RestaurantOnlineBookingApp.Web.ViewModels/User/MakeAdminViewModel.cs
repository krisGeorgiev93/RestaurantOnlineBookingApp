using System.ComponentModel.DataAnnotations;

namespace RestaurantOnlineBookingApp.Web.ViewModels.User
{
    public class MakeAdminViewModel
    {
        [Required]
        [EmailAddress]
        public string UserEmail { get; set; } = null!;
    }
}
