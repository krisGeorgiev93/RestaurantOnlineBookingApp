using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
            this.Reviews = new List<Review>();
            this.Meals = new List<Meal>();
            this.CapacityPerDates = new List<CapacityPerDate>();
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
        public TimeSpan StartingTime { get; set; }

        [Required]
        public TimeSpan EndingTime { get; set; }

        [Required]
        [Range(1, 300)]
        public int Capacity { get; set; }

        public bool IsActive { get; set; } = true;

        [Required]
        public int CityId { get; set; }

        [ForeignKey(nameof(CityId))]
        public City City { get; set; } = null!;

        [Required]
        public int CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public virtual Category Category { get; set; } = null!;

        [Required]
        public Guid OwnerId { get; set; }
        [ForeignKey(nameof(OwnerId))]
        public virtual Owner Owner { get; set; } = null!;

        public Guid? GuestId { get; set; }

        [ForeignKey(nameof(GuestId))]
        public virtual AppUser? Guest { get; set; }

        public ICollection<Meal> Meals { get; set; }

        public ICollection<RestaurantGuest> RestaurantGuests { get; set; }

        public ICollection<Review> Reviews { get; set; }

        public ICollection<Booking> Bookings { get; set; }

        public ICollection<CapacityPerDate> CapacityPerDates { get; set; }
        public double Rating => Reviews.Count > 0 ? Reviews.Sum(r=> r.ReviewRating) / Reviews.Count : 0;

    }
}
