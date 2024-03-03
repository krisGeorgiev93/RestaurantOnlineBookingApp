using RestaurantOnlineBookingApp.Data.Models;
using RestaurantOnlineBookingApp.Web.ViewModels.Booking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantOnlineBooking.Services.Data.Interfaces
{
    public interface IBookingService
    {
        Task<bool> BookTableAsync(string restaurantId, BookingFormViewModel model,string userId);

        Task<IEnumerable<BookingAllViewModel>> GetBookingsByUserIdAsync(string userId);

        Task DeleteBookingAsync(string bookingId);

        Task<BookingFormViewModel> GetBookingByIdAsync(string bookingId);

        Task<bool> BookingExistsByIdAsync(string bookingId);

    }
}
