
namespace RestaurantOnlineBookingApp.Data
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using RestaurantOnlineBookingApp.Data.Models;

    public class RestaurantOnlineBookingAppDbContext : IdentityDbContext<CustomUser, IdentityRole<Guid>, Guid>
    {
        public RestaurantOnlineBookingAppDbContext(DbContextOptions<RestaurantOnlineBookingAppDbContext> options)
            : base(options)
        {
        }
    }
}
