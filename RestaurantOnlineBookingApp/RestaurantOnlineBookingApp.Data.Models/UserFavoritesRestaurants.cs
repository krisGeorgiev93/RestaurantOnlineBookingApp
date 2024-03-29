using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantOnlineBookingApp.Data.Models
{
    public class UserFavoritesRestaurants
    {
        public Guid UserId { get; set; }
        public Guid RestaurantId { get; set; }

        [ForeignKey(nameof(UserId))]
        public AppUser User { get; set; }

        [ForeignKey(nameof(RestaurantId))]
        public Restaurant Restaurant { get; set; }
      
    }
}
