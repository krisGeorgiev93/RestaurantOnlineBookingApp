using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantOnlineBookingApp.Web.ViewModels.Review
{
    public class ReviewAllViewModel
    {
        public Guid Id { get; set; }
        public int ReviewRating { get; set; }
        public string Comment { get; set; }
        public Guid GuestId { get; set; }
        //public string GuestName { get; set; }   
        public Guid RestaurantId { get; set; }

    }
}
