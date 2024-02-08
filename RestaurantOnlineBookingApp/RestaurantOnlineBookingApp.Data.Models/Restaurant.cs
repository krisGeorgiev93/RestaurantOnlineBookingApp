using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantOnlineBookingApp.Data.Models
{
    using static RestaurantOnlineBookingApp.Common.ValidationConstants.Restaurant;
    public class Restaurant
    {
        
        public Restaurant()
        {
            this.Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;
        [Required]
        [MaxLength(AddressMaxLength)]
        public string Address { get; set; } = null!;
        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [Required]
        [MaxLength(ImageUrlMaxLength)]
        public string ImageUrl { get; set; } = null!;

        [Required]
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; } = null!;

        [Required]
        public Guid OwnerId { get; set; }

        public virtual Owner Owner { get; set; } = null!;

        public Guid? GuestId { get; set; }

        public virtual CustomUser? Guest { get; set; }

    }
}
