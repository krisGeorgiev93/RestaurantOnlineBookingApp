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
            this.Meals = new List<Meal>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public Guid RestaurantId { get; set; }
        [ForeignKey(nameof(RestaurantId))]
        public Restaurant Restaurant { get; set; } = null!;

        public ICollection<Meal> Meals { get; set; } = null!;
    }
}
