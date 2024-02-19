﻿
using System.ComponentModel.DataAnnotations;

namespace RestaurantOnlineBookingApp.Web.ViewModels.Restaurant
{
    public class AllRestaurantsQueryModel
    {
        public AllRestaurantsQueryModel()
        {
            CurrentPage = 1;
            RestaurantsPerPage = 5;
            this.Categories = new HashSet<string>();
            this.Restaurants = new HashSet<RestaurantAllViewModel>();
        }
        public string Category { get; set; }

        [Display(Name = "Search by word")]
        public string Search { get; set; }

        [Display(Name = "Sort restaurants by")]

        public int TotalRestaurants { get; set; }

        [Display(Name = "Restaurants On Page")]
        public int RestaurantsPerPage { get; set; }

        public int CurrentPage {  get; set; }

        public IEnumerable<string> Categories { get; set; }

        public IEnumerable<RestaurantAllViewModel> Restaurants { get; set; }
    }
}
