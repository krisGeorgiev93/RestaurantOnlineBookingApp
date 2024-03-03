using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantOnlineBookingApp.Web.ViewModels.Booking
{
    public class BookingAllViewModel
    {
        [Display(Name = "Booking Date")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public string BookingDate { get; set; }
        public string ReservedTime { get; set; }
        public int NumberOfGuests { get; set; }
        public string RestaurantName { get; set; }

        public string ImageUrl { get; set; }
        public Guid RestaurantId {  get; set; }
    }
}
