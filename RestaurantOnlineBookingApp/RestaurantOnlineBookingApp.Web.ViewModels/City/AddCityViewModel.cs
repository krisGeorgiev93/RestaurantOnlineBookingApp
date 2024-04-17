using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantOnlineBookingApp.Web.ViewModels.City
{
    public class AddCityViewModel
    {
        [Required]
        public string Name { get; set; } = null!;
    }
}
