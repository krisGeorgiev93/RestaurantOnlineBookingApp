using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantOnlineBookingApp.Web.ViewModels.Category
{
    public class AddCategoryViewModel
    {
        [Required]
        public string Name { get; set; } = null!;
    }
}
