using RestaurantOnlineBookingApp.Data.Models;
using RestaurantOnlineBookingApp.Web.ViewModels.Event;

namespace RestaurantOnlineBooking.Services.Data.Interfaces
{
    public interface IEventService
    {
        Task CreateEventAsync(EventFormModel model, string restaurantId);
        Task<EventFormModel> GetEventByIdAsync(string eventId);
        Task EditEventAsync(EventFormModel model);
        Task<IEnumerable<Event>> GetAllEventsByRestaurantIdAsync(string restaurantId);

        Task<bool> EventExistsByIdAsync(string eventId);

        Task DeleteEventAsync(string eventId);
    }
}
