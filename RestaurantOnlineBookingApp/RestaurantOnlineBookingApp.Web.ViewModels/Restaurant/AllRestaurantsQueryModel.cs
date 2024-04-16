namespace RestaurantOnlineBookingApp.Web.ViewModels.Restaurant
{
    using System.ComponentModel.DataAnnotations;
    using static Common.ApplicationConstants;
    public class AllRestaurantsQueryModel
    {
        public AllRestaurantsQueryModel()
        {
            CurrentPage = DefaultPage;
            RestaurantsPerPage = EntitiesPerPage;
            this.Categories = new HashSet<string>();
            this.Restaurants = new HashSet<RestaurantAllViewModel>();
            this.Cities = new HashSet<string>();
        }
        public string Category { get; set; }

        public string City { get; set; }

        [Display(Name = "Search by word")]
        public string Search { get; set; }

        [Display(Name = "Sort restaurants by")]

        public int TotalRestaurants { get; set; }

        [Display(Name = "Restaurants On Page")]
        public int RestaurantsPerPage { get; set; }

        public SortOption SortBy { get; set; }
        public double? Rating { get; set; }
        public int CurrentPage { get; set; }
        public bool SortByRating { get; set; }

        public IEnumerable<string> Categories { get; set; }

        public IEnumerable<string> Cities { get; set; }

        public IEnumerable<RestaurantAllViewModel> Restaurants { get; set; }
    }
}
