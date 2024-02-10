
namespace RestaurantOnlineBookingApp.Data
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using RestaurantOnlineBookingApp.Data.Models;

    public class RestaurantBookingDbContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>
    {
        public RestaurantBookingDbContext(DbContextOptions<RestaurantBookingDbContext> options)
            : base(options)
        {
        }
    }
}
