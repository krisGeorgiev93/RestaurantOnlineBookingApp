using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantOnlineBookingApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RestaurantOnlineBookingApp.Common.ValidationConstants;
using Event = RestaurantOnlineBookingApp.Data.Models.Event;

namespace RestaurantOnlineBookingApp.Data.Configurations
{
    public class EventEntityConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.HasData(this.UploadEvents());
                        
        }

        private Event[] UploadEvents()
        {
            ICollection<Event> events = new HashSet<Event>();
            Event @event;

            @event = new Event()
            {
                Title = "Special Event 1",
                Date = DateTime.Now.Date.AddDays(5),
                Time = new TimeSpan(18, 0, 0),
                Description = "Description for Special Event 1",
                Price = 50.00m,
                ImageUrl = "https://www.eventbookings.com/wp-content/uploads/2018/03/event-ideas-for-party-eventbookings.jpg",
                RestaurantId = Guid.Parse("E681A1DD-B85F-4860-8EB9-CF517E5C4FE8")
            };
            events.Add(@event);

            return events.ToArray();           
        }
    }
}
