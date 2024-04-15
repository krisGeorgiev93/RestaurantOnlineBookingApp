namespace RestaurantOnlineBooking.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using RestaurantOnlineBooking.Services.Data.Interfaces;
    using RestaurantOnlineBookingApp.Data;
    using RestaurantOnlineBookingApp.Data.Models;
    using RestaurantOnlineBookingApp.Web.ViewModels.Booking;
    using System.Globalization;

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

            TimeSpan reservedTime;
            if (!TimeSpan.TryParse(model.ReservedTime, out reservedTime))
            {
                throw new FormatException("Invalid format for ReservedTime");
            }

            DateTime bookingDate;
            if (!DateTime.TryParseExact(model.BookingDate, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out bookingDate))
            {
                throw new FormatException("Invalid format for BookingDate");
            }

            // Check if the booking date is in the past
            //if (bookingDate.Date < DateTime.Now.Date || (bookingDate.Date == DateTime.Now.Date && reservedTime < DateTime.Now.TimeOfDay))
            //{
            //    throw new InvalidOperationException("Cannot book for past dates or times.");
            //}

            // Проверка за наличност на капацитета за конкретната дата
            var capacityForDate = await this.dBContext.CapacitiesParDate
                .FirstOrDefaultAsync(c => c.RestaurantId == restaurantGuid && c.Date == bookingDate);

            var booking = new Booking
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                NumberOfGuests = model.NumberOfGuests,
                PhoneNumber = model.PhoneNumber,
                BookingDate = bookingDate,
                ReservedTime = reservedTime,
                RestaurantId = restaurantGuid,
                GuestId = Guid.Parse(userId)
            };

            await this.dBContext.Bookings.AddAsync(booking);
            // Намаляване на капацитета за конкретната дата
            capacityForDate.Capacity -= model.NumberOfGuests;

            await this.dBContext.SaveChangesAsync();

            return true;
        }

        public async Task DeleteBookingAsync(string bookingId)
        {
            var booking = await this.dBContext
                  .Bookings
                  .Include(b => b.Restaurant)
                  .FirstOrDefaultAsync(b => b.Id.ToString() == bookingId);

            if (booking == null)
            {
                throw new InvalidOperationException("Booking not found.");
            }
            // Increase the restaurant's capacity by the number of guests from the deleted booking
            // booking.Restaurant.Capacity += booking.NumberOfGuests;
            var capacityForDate = await this.dBContext.CapacitiesParDate
                .FirstOrDefaultAsync(c => c.RestaurantId == booking.RestaurantId && c.Date == booking.BookingDate);
            if (capacityForDate != null)
            {
                // Add the capacity back to the date
                capacityForDate.Capacity += booking.NumberOfGuests;
            }

            this.dBContext.Bookings.Remove(booking);
            await this.dBContext.SaveChangesAsync();
        }

        public async Task<BookingFormViewModel> GetBookingByIdAsync(string bookingId)
        {
            var booking = await this.dBContext.Bookings.FirstAsync(b => b.Id.ToString() == bookingId);

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
                ReservedTime = booking.ReservedTime.ToString(),
                BookingDate = booking.BookingDate.ToString(),
                NumberOfGuests = booking.NumberOfGuests,

            };

            return bookingModel;
        }

        public async Task<IEnumerable<BookingAllViewModel>> GetBookingsByRestaurantIdAsync(string restaurantId)
        {
            // Проверка за валидност на restaurantId
            if (!Guid.TryParse(restaurantId, out Guid restaurantGuid))
            {
                throw new ArgumentException("Invalid restaurantId");
            }

            // Извличане на всички резервации за дадения ресторант
            var bookings = await this.dBContext.Bookings
                .Include(b => b.Restaurant)
                .Where(b => b.RestaurantId == restaurantGuid && b.BookingDate >= DateTime.Today)
                .ToListAsync();

            return bookings.Select(b => new BookingAllViewModel
            {
                Id = b.Id,
                BookingDate = b.BookingDate.Date.ToString("dd-MM-yyyy"),
                ReservedTime = b.ReservedTime.ToString(@"hh\:mm"),
                NumberOfGuests = b.NumberOfGuests,
                RestaurantId = b.RestaurantId,
                RestaurantName = b.Restaurant.Name,
                ImageUrl = b.Restaurant.ImageUrl,
                GuestEmail = b.Email,
                GuestName = b.FirstName + " " + b.LastName
            }).ToList();
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

        public List<string> GetReservedTimes(TimeSpan startingTime, TimeSpan endingTime, TimeSpan interval)
        {
            var reservedTimes = new List<string>();
            var currentTime = startingTime;

            while (currentTime <= endingTime)
            {
                reservedTimes.Add(currentTime.ToString("hh\\:mm"));
                currentTime = currentTime.Add(interval);
            }

            return reservedTimes;
        }

        public async Task<bool> HasValidReservationAsync(Guid restaurantId, Guid guestId)
        {
            // Извличане на текущата дата и час
            DateTime currentDate = DateTime.Now;

            // Проверка за наличие на резервация за дадения ресторант и гост
            var reservation = await this.dBContext.Bookings
                .FirstOrDefaultAsync(r => r.RestaurantId == restaurantId &&
                                          r.GuestId == guestId);

            // Ако няма резервация, връщаме false
            if (reservation == null)
            {
                return false;
            }

            // Проверка дали резервацията е с минала дата
            if (reservation.BookingDate.Date < currentDate.Date ||
                (reservation.BookingDate.Date == currentDate.Date && 
                reservation.ReservedTime < currentDate.TimeOfDay))
            {
                return true;
            }

            return false;
        }
    }
}