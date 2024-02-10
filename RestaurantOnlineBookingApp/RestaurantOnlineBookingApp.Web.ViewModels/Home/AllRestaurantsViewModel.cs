using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantOnlineBookingApp.Web.ViewModels.Home
{
    public class AllRestaurantsViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string Address { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public double Rating { get; set; }

        public string City { get; set; }

        public int Capacity { get; set; }
    }
}
