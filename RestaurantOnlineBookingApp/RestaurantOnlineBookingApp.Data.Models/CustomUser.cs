using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantOnlineBookingApp.Data.Models
{
    public class CustomUser : IdentityUser<Guid>
    {
        public CustomUser()
        {
            this.Restaurants = new HashSet<Restaurant>();
        }

        public virtual ICollection<Restaurant> Restaurants { get; set; }
    }
}
