using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantOnlineBookingApp.Data.Models
{
    public class MenuMeal
    {
        public int MenuId { get; set; }

        [ForeignKey(nameof(MenuId))]
        [Required]
        public Menu Menu { get; set; } = null!;
        public int MealId { get; set; }

        [ForeignKey(nameof(MealId))]
        [Required]
        public Meal Meal { get; set; } = null!;

    }
}
