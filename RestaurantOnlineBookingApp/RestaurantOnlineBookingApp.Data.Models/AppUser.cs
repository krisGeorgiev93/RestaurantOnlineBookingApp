using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantOnlineBookingApp.Data.Models
{
    public class AppUser : IdentityUser<Guid>
    {
        public AppUser()
        {
            this.Id = Guid.NewGuid();
            this.BookedRestaurants = new List<Restaurant>();
            this.FavoriteRestaurants = new List<Restaurant>();
        }       
        public string? FirstName { get; set; }
       
        public string? LastName { get; set; }

        [NotMapped]
        public  ICollection<Restaurant> BookedRestaurants { get; set; }
        [NotMapped]
        public ICollection<Restaurant> FavoriteRestaurants { get; set; }
    }
}
