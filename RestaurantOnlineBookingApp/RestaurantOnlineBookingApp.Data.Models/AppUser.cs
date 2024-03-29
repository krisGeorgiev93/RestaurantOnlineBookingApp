
namespace RestaurantOnlineBookingApp.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations;
    using static RestaurantOnlineBookingApp.Common.ValidationConstants.AppUser;

    public class AppUser : IdentityUser<Guid>
    {
        public AppUser()
        {
            this.Id = Guid.NewGuid();            
            this.FavoriteRestaurants = new HashSet<Restaurant>();
            this.IsDeleted = false;
        }

        [Required]
        [MaxLength(FirstNameMaxLength)]
        public string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(LastNameMaxLength)]
        public string LastName { get; set; } = null!;

        public bool IsDeleted { get; set; }

        // Колекция, която ще съхранява любимите ресторанти на потребителя
        public ICollection<Restaurant> FavoriteRestaurants { get; set; }
    }
}
