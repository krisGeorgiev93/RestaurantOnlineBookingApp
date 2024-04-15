namespace RestaurantOnlineBookingApp.Data.Configurations
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using RestaurantOnlineBookingApp.Data.Models;

    public class SeedRolesAndAdmin
    {
        public static void SeedAdmin(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

                SeedRoles(roleManager);
                SeedAdminUser(userManager);
            }
        }

        private static void SeedRoles(RoleManager<IdentityRole<Guid>> roleManager)
        {
            // Check if Admin role exists, if not, create it
            if (!roleManager.RoleExistsAsync("Admin").Result)
            {
                var role = new IdentityRole<Guid>("Admin");
                roleManager.CreateAsync(role).Wait();
            }
        }

        private static void SeedAdminUser(UserManager<AppUser> userManager)
        {
            // Check if the admin user already exists
            if (userManager.FindByEmailAsync("admintest@mail.com").Result == null)
            {
                // Create a new admin user
                var user = new AppUser
                {
                    UserName = "admintest@mail.com",
                    Email = "admintest@mail.com",
                    FirstName = "Admin",
                    LastName = "Test",
                    LockoutEnabled = false,
                };

                // Add the admin user with a predefined password
                var result = userManager.CreateAsync(user, "1234567").Result;

                if (result.Succeeded)
                {
                    // Assign the Admin role to the admin user
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }
        }
    }
}
