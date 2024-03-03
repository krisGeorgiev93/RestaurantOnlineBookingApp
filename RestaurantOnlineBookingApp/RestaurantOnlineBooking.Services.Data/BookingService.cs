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

namespace RestaurantOnlineBooking.Services.Data
{
    public class BookingService : IBookingService
    {
        private readonly RestaurantBookingDbContext dBContext;

        public BookingService(RestaurantBookingDbContext dBContext)
        {
            this.dBContext = dBContext;
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

        public async Task<IEnumerable<BookingAllViewModel>> GetBookingsByUserIdAsync(string userId)
        {
            var bookings = await this.dBContext.Bookings
                .Include(b => b.Restaurant) // Include Restaurant entity to access its properties
                .Where(b => b.GuestId.ToString() == userId)
                 .ToListAsync();

            return bookings.Select(b => new BookingAllViewModel
            {
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