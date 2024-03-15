using System.Security.Claims;
namespace RestaurantOnlineBookingApp.Infrastructure.Extensions
{
    public static class ClaimsPrincipalsExtensions
    {
        public static string? GetId(this ClaimsPrincipal user)
        {
            return user.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}