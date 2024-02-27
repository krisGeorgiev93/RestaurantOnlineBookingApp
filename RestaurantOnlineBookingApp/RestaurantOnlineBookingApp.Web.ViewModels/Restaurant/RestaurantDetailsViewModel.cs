using RestaurantOnlineBookingApp.Web.ViewModels.Owner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantOnlineBookingApp.Web.ViewModels.Restaurant
{
    public class RestaurantDetailsViewModel : RestaurantAllViewModel
    {
        public string Category { get; set; } = null!;

        public string OpeningTime { get; set; } = null!;
        public string ClosingTime {  get; set; } = null!;
        public OwnerInfoOnRestaurantViewModel OwnerInfo { get; set; } = null!;

    }
}
