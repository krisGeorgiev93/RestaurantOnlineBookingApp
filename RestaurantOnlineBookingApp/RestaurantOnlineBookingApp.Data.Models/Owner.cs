using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantOnlineBookingApp.Data.Models
{
    using static RestaurantOnlineBookingApp.Common.ValidationConstants.Owner;
    public class Owner
    {
        public Owner()
        {
            this.Id = Guid.NewGuid();
            this.OwnedRestaurants = new HashSet<Restaurant>();
        }

        [Key]
        public Guid Id { get; set; }        

        [Required]
        [MaxLength(PhoneNumberMaxLength)]
        public string PhoneNumber { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual AppUser User { get; set; } = null!;

        public virtual ICollection<Restaurant> OwnedRestaurants { get; set; }
    }
}
