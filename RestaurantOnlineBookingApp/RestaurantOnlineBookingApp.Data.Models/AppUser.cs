using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace RestaurantOnlineBookingApp.Data.Models
{
    public class AppUser : IdentityUser<Guid>
    {
        public AppUser()
        {
            this.Id = Guid.NewGuid();
            this.BookedRestaurants = new HashSet<Restaurant>();
        }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
        public virtual ICollection<Restaurant> BookedRestaurants { get; set; }
    }
}
