using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static RestaurantOnlineBookingApp.Common.ValidationConstants.Meal;
namespace RestaurantOnlineBookingApp.Data.Models
{
    public class Meal
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;
        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string ImageUrl { get; set; } = null!;

        public Guid? RestaurantId { get; set; }

        [ForeignKey(nameof(RestaurantId))]       
        public Restaurant? Restaurant { get; set; } 
    }
}
