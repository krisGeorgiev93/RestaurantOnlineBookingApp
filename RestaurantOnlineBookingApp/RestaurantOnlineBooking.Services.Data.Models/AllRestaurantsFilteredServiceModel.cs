using RestaurantOnlineBookingApp.Web.ViewModels.Restaurant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantOnlineBooking.Services.Data.Models
{
    //Dto between model and controller
    public class AllRestaurantsFilteredServiceModel
    {
        public AllRestaurantsFilteredServiceModel()
        {
            this.Restaurants = new HashSet<RestaurantAllViewModel>();
        }
        public int TotalRestaurantsCount { get; set; }

        public IEnumerable<RestaurantAllViewModel> Restaurants { get; set; }
    }
}
