using System.ComponentModel.DataAnnotations;

namespace RestaurantOnlineBookingApp.Data.Models
{
    using static RestaurantOnlineBookingApp.Common.ValidationConstants.Category;
    public class Category
    {
        public Category()
        {
            this.Restaurants = new HashSet<Restaurant>();
        }
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        public virtual ICollection<Restaurant> Restaurants { get; set; } = null!;
    }
}
