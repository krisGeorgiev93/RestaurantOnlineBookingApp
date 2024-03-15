
namespace RestaurantOnlineBookingApp.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static RestaurantOnlineBookingApp.Common.ValidationConstants.AppUser;

    public class AppUser : IdentityUser<Guid>
    {
        public AppUser()
        {
            this.Id = Guid.NewGuid();
            this.BookedRestaurants = new List<Restaurant>();
            this.FavoriteRestaurants = new List<Restaurant>();
        }

        [Required]
        [MaxLength(FirstNameMaxLength)]
        public string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(LastNameMaxLength)]
        public string LastName { get; set; } = null!;

        [NotMapped]
        public  ICollection<Restaurant> BookedRestaurants { get; set; }

        [NotMapped]
        public ICollection<Restaurant> FavoriteRestaurants { get; set; }
    }
}
