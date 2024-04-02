using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantOnlineBookingApp.Web.ViewModels.Review
{
    public class ReviewViewModel
    {
        public IEnumerable<ReviewAllViewModel> Reviews { get; set; }
        public SortOption SortBy { get; set; } // Assuming SortOption is available in this scope
    }
}
