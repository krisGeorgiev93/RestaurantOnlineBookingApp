namespace RestaurantOnlineBookingApp.Web.ViewModels.Review
{
    using System.ComponentModel.DataAnnotations;
    using static RestaurantOnlineBookingApp.Common.ValidationConstants.Review;
    public class AddReviewViewModel
    {

        [Range(ReviewRatingMinValue, ReviewRatingMaxValue)]
        [Required]
        public int Rating { get; set; }

        [MinLength(CommentMinLength)]
        [MaxLength(CommentMaxLength)]
        [Required]
        public string Comment { get; set; } = null!;

        public DateTime CreatedAt { get; set; }
        public Guid RestaurantId { get; set; }

        public Guid GuestId { get; set; }

        public string GuestEmail { get; set; }

    }
}
