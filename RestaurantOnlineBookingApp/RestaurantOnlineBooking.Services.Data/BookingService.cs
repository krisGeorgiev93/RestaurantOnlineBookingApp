using Microsoft.EntityFrameworkCore;
using RestaurantOnlineBooking.Services.Data.Interfaces;
using RestaurantOnlineBookingApp.Data;
using RestaurantOnlineBookingApp.Data.Models;
using RestaurantOnlineBookingApp.Web.ViewModels.Booking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static RestaurantOnlineBookingApp.Common.ValidationConstants;

namespace RestaurantOnlineBooking.Services.Data
{
    public class BookingService : IBookingService
    {
        private readonly RestaurantBookingDbContext dBContext;

        public BookingService(RestaurantBookingDbContext dBContext)
        {
            this.dBContext = dBContext;
        }

        public async Task<bool> BookingExistsByIdAsync(string bookingId)
        {
            bool IsExists = await this.dBContext
                .Bookings
                .AnyAsync(r => r.Id.ToString() == bookingId);

            return IsExists;
        }

        public async Task<bool> BookTableAsync(string restaurantId, BookingFormViewModel model, string userId)
        {
            if (!Guid.TryParse(restaurantId, out Guid restaurantGuid))
            {
                throw new ArgumentException("Invalid restaurantId");
            }

            var restaurant = await this.dBContext.Restaurants.FindAsync(restaurantGuid);

            if (restaurant == null)
            {
                throw new ArgumentException("Invalid restaurant ID");
            }

            // Check if the requested number of guests exceeds the restaurant's capacity
            if (model.NumberOfGuests > restaurant.Capacity)
            {
                throw new InvalidOperationException("Number of guests exceeds restaurant capacity");
            }

            restaurant.Capacity -= model.NumberOfGuests;

            var booking = new Booking
            {                
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                NumberOfGuests = model.NumberOfGuests,
                PhoneNumber = model.PhoneNumber,
                BookingDate = model.BookingDate,
                ReservedTime = model.ReservedTime,
                RestaurantId = restaurantGuid,
                GuestId = Guid.Parse(userId)
            };

            await this.dBContext.Bookings.AddAsync(booking);
            await this.dBContext.SaveChangesAsync();

            return true;


        }

        public async Task DeleteBookingAsync(string bookingId)
        {
            var booking = await this.dBContext
                  .Bookings.FindAsync(Guid.Parse(bookingId));

            if (booking == null)
            {
                throw new InvalidOperationException("Booking not found.");
            }

            this.dBContext.Bookings.Remove(booking);
            await this.dBContext.SaveChangesAsync();
        }

        public async Task<BookingFormViewModel> GetBookingByIdAsync(string bookingId)
        {
            var booking = await this.dBContext.Bookings.FirstAsync(b=> b.Id.ToString() == bookingId);

            if (booking == null)
            {
                throw new InvalidOperationException("Booking not found.");
            }

            var bookingModel = new BookingFormViewModel()
            {                
                FirstName = booking.FirstName,
                LastName = booking.LastName,
                PhoneNumber = booking.PhoneNumber,
                Email = booking.Email,
                ReservedTime = booking.ReservedTime,
                BookingDate = booking.BookingDate,
                NumberOfGuests = booking.NumberOfGuests,              

            };

            return bookingModel;
        }

        public async Task<IEnumerable<BookingAllViewModel>> GetBookingsByUserIdAsync(string userId)
        {
            var bookings = await this.dBContext.Bookings
                .Include(b => b.Restaurant) // Include Restaurant entity to access its properties
                .Where(b => b.GuestId.ToString() == userId)
                 .ToListAsync();

            return bookings.Select(b => new BookingAllViewModel
            {
                Id = b.Id, //this id have to be set because of delete action
                BookingDate = b.BookingDate.Date.ToString("dd-MM-yyyy"),
                ReservedTime = b.ReservedTime.ToString(@"hh\:mm"),
                NumberOfGuests = b.NumberOfGuests,
                RestaurantId = b.RestaurantId,
                RestaurantName = b.Restaurant.Name, // Assuming Restaurant has a Name property
                ImageUrl = b.Restaurant.ImageUrl // Assuming Restaurant has an ImageUrl property
            }).ToList();
        }
    }
}