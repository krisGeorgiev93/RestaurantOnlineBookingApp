using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantOnlineBookingApp.Data.Models
{
    public class CapacityPerDate
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Capacity { get; set; }
        public Guid RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }
    }
}
