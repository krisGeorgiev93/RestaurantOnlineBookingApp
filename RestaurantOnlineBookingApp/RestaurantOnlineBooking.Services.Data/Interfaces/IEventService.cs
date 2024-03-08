using RestaurantOnlineBookingApp.Web.ViewModels.Event;

namespace RestaurantOnlineBooking.Services.Data.Interfaces
{
    public interface IEventService
    {
        Task CreateEventAsync(EventFormModel model, string restaurantId);

    }
}
