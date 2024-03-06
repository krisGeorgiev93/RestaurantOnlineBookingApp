using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantOnlineBookingApp.Web.ViewModels.Review
{
    public class ReviewDetailsViewModel
    {
        public int ReviewRating { get; set; }
        public string Comment { get; set; }
        public string RestaurantName { get; set; }
        public Guid RestaurantId { get; set; }
    }
}
