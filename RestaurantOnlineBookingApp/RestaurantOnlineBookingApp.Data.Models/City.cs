﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantOnlineBookingApp.Data.Models
{
    public class City
    {

        public int Id { get; set; }

        [Required]
        public string CityName { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public virtual ICollection<Restaurant> Restaurants { get; set; } = null!;

    }
}
