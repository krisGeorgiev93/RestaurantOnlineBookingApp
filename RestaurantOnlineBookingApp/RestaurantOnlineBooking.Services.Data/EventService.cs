using Microsoft.EntityFrameworkCore;
using RestaurantOnlineBooking.Services.Data.Interfaces;
using RestaurantOnlineBookingApp.Data;
using RestaurantOnlineBookingApp.Data.Models;
using RestaurantOnlineBookingApp.Web.ViewModels.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantOnlineBooking.Services.Data
{
    public class EventService : IEventService
    {
        private readonly RestaurantBookingDbContext _dbContext;
        private readonly IOwnerService _ownerService;
        public EventService(RestaurantBookingDbContext dbContext, IOwnerService ownerService)
        {
            _dbContext = dbContext;
            _ownerService = ownerService;
        }

       

        public async Task CreateEventAsync(EventFormModel model, string restaurantId)
        {
            var restaurant = await this._dbContext.Restaurants.FirstOrDefaultAsync(r => r.Id.ToString() == restaurantId);

            if (restaurant == null)
            {
                throw new ArgumentException("Restaurant not found or user is not the owner.");
            }
            var @event = new Event
            {
                RestaurantId = model.RestaurantId,
                Title = model.Title,
                Date = model.Date,
                Description = model.Description,
                Price = model.Price,
                ImageUrl = model.ImageUrl
            };

            await _dbContext.Events.AddAsync(@event);
            await _dbContext.SaveChangesAsync();
        }
    }
}
