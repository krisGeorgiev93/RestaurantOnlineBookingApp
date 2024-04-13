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
                RestaurantId = Guid.Parse("3614f373-2355-4e6c-96e5-542f0689752f")
            };
            events.Add(@event);

            return events.ToArray();           
        }
    }
}
