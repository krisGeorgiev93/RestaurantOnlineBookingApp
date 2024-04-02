namespace RestaurantOnlineBookingApp.Web.ViewModels.Review
{
    public class ReviewAllViewModel
    {
        public Guid Id { get; set; }
        public int ReviewRating { get; set; }
        public string Comment { get; set; }
        public Guid GuestId { get; set; }
        public string GuestEmail { get; set; }   
        public string FirstName {  get; set; }
        public string LastName { get; set; }
       public SortOption SortBy { get; set; }
        public Guid RestaurantId { get; set; }
        public DateTime CreatedAt { get; set; }
        

    }
}
