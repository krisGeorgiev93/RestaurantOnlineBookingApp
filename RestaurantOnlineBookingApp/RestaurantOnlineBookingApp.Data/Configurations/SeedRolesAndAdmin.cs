namespace RestaurantOnlineBookingApp.Data.Configurations
{
    using Microsoft.AspNetCore.Identity;
    using RestaurantOnlineBookingApp.Data.Models;

    public class SeedRolesAndAdmin
    {
        public void SeedAdmin(UserManager<AppUser> userManager, RoleManager<IdentityRole<Guid>> roleManager)
        {
            SeedRoles(roleManager);
            SeedAdminUser(userManager);
        }

        private void SeedRoles(RoleManager<IdentityRole<Guid>> roleManager)
        {
            // Check if Admin role exists, if not, create it
            if (!roleManager.RoleExistsAsync("Admin").Result)
            {
                var role = new IdentityRole<Guid>("Admin");
                roleManager.CreateAsync(role).Wait();
            }
        }

        private void SeedAdminUser(UserManager<AppUser> userManager)
        {
            // Check if the admin user already exists
            if (userManager.FindByEmailAsync("admin@test.com").Result == null)
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
