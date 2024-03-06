using System.ComponentModel.DataAnnotations;
using static RestaurantOnlineBookingApp.Common.ValidationConstants.City;
namespace RestaurantOnlineBookingApp.Data.Models
{
    public class City
    {

        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string CityName { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public virtual ICollection<Restaurant> Restaurants { get; set; } = null!;

    }
}
