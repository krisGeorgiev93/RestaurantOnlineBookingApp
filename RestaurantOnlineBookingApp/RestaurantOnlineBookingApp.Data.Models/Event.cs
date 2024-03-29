﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static RestaurantOnlineBookingApp.Common.ValidationConstants.Event;
namespace RestaurantOnlineBookingApp.Data.Models
{
    public class Event
    {
        public Event()
        {
            this.Id = Guid.NewGuid();           
        }

        public Guid Id { get; set; }
        [Required]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; }
        [Required]
        public DateTime Date { get; set; }

        [Required]  
        public TimeSpan Time { get; set; }

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }
        public decimal Price {  get; set; }
        public string ImageUrl {  get; set; }
        public Guid RestaurantId { get; set; }

        [ForeignKey(nameof(RestaurantId))]
        [Required]
        public Restaurant Restaurant { get; set; }

    }
}

