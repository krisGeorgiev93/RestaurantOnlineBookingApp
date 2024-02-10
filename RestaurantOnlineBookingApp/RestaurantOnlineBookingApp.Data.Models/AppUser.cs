using Microsoft.AspNetCore.Identity;

namespace RestaurantOnlineBookingApp.Data.Models
{
    public class AppUser : IdentityUser<Guid>
    {
        public AppUser()
        {
            this.BookedRestaurants = new HashSet<Restaurant>();
        }


        public virtual ICollection<Restaurant> BookedRestaurants { get; set; }
    }
}
