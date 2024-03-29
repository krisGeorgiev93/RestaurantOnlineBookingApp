﻿namespace RestaurantOnlineBooking.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using RestaurantOnlineBooking.Services.Data.Interfaces;
    using RestaurantOnlineBookingApp.Data;
    using RestaurantOnlineBookingApp.Web.ViewModels.Event;
    public class EventService : IEventService
    {
        private readonly RestaurantBookingDbContext _dbContext;
        private readonly IPhotoService _photoService;
        public EventService(RestaurantBookingDbContext dbContext, IOwnerService ownerService, IPhotoService photoService)
        {
            _dbContext = dbContext;
            _photoService = photoService;
        }      

        public async Task CreateEventAsync(EventFormModel model, string restaurantId)
        {
            var restaurant = await this._dbContext.Restaurants.FirstOrDefaultAsync(r => r.Id.ToString() == restaurantId);

            if (restaurant == null)
            {
                throw new ArgumentException("Restaurant not found or user is not the owner.");
            }

            var photo = await this._photoService.AddPhotoAsync(model.Image);

            var @event = new RestaurantOnlineBookingApp.Data.Models.Event
            {
                RestaurantId = model.RestaurantId,
                Title = model.Title,
                Date = model.Date,
                Time = model.Time,
                Description = model.Description,
                Price = model.Price,
                ImageUrl = photo.Url.ToString()
            };

            await _dbContext.Events.AddAsync(@event);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteEventAsync(string eventId)
        {
            var eventForDelete = await this._dbContext
                  .Events.FirstAsync(m => m.Id.ToString() == eventId);

            if (eventForDelete == null)
            {
                throw new InvalidOperationException("Event not found.");
            }

            this._dbContext.Events.Remove(eventForDelete);

            await this._dbContext.SaveChangesAsync();
        }

        public async Task EditEventAsync(EventFormModel model)
        {
            var existingEvent = await _dbContext.Events.FindAsync(model.Id);

            if (existingEvent == null)
            {
                throw new ArgumentException("Event not found.");
            }

            var photo = await this._photoService.AddPhotoAsync(model.Image);

            // Update event properties
            existingEvent.Title = model.Title;
            existingEvent.Date = model.Date;
            existingEvent.Time = model.Time;
            existingEvent.Description = model.Description;
            existingEvent.Price = model.Price;
            existingEvent.ImageUrl = photo.Url.ToString();

            await _dbContext.SaveChangesAsync();

        }

        public async Task<bool> EventExistsByIdAsync(string eventId)
        {
            bool IsExists = await this._dbContext
              .Events
              .AnyAsync(m => m.Id.ToString() == eventId);

            return IsExists;
        }

        public async Task<IEnumerable<RestaurantOnlineBookingApp.Data.Models.Event>> GetAllEventsByRestaurantIdAsync(string restaurantId)
        {
            return await _dbContext.Events
           .Where(e => e.RestaurantId.ToString() == restaurantId)
           .ToListAsync();
        }

        public async Task<EventFormModel> GetEventByIdAsync(string eventId)
        {
            var @event = await this._dbContext.Events.FirstAsync(m => m.Id.ToString() == eventId);

            if (@event == null)
            {
                throw new InvalidOperationException("Event not found.");
            }

            var eventForm = new EventFormModel()
            {
                Id = @event.Id,
                Title = @event.Title,
                Description = @event.Description,
               // Image = @event.ImageUrl.ToString(),
                Date = @event.Date,
                Time = @event.Time,
                Price = @event.Price,
                RestaurantId = (Guid)@event.RestaurantId
            };

            return eventForm;
        }
    }
}
