using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantOnlineBookingApp.Data.Models
{
    public class Menu
    {
        public Menu()
        {
            this.MenuMeals = new List<MenuMeal>();
            this.Restaurants = new List<Restaurant>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<MenuMeal> MenuMeals { get; set; } = null!;

        public ICollection<Restaurant> Restaurants { get; set; } = null!;
    }
}
