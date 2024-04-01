using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static RestaurantOnlineBookingApp.Common.ValidationConstants.Review;

namespace RestaurantOnlineBookingApp.Data.Models
{
    public class Review
    {
        public Review()
        {
            this.Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }

        public int ReviewRating { get; set; }

        [MaxLength(CommentMaxLength)]
        public string Comment {  get; set; }

        public Guid GuestId { get; set; }

        [ForeignKey(nameof(GuestId))]
        public AppUser Guest { get; set; }
        public Guid RestaurantId { get; set; }

        [ForeignKey(nameof(RestaurantId))]
        public Restaurant Restaurant { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
