using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantOnlineBookingApp.Data.Models
{
    public class Booking
    {
        public Guid Id { get; set; }

        public DateTime BookingTime { get; set; }

        public int NumberOfGuests { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public Guid GuestId { get; set; }

        [ForeignKey(nameof(GuestId))]
        public AppUser Guest { get; set; }

    }
}
