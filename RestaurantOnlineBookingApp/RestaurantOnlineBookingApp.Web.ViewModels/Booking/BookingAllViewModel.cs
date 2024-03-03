using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantOnlineBookingApp.Web.ViewModels.Booking
{
    public class BookingAllViewModel
    {
        public DateTime BookingDate { get; set; }
        public TimeSpan ReservedTime { get; set; }
        public int NumberOfGuests { get; set; }
        public string RestaurantName { get; set; }
    }
}
