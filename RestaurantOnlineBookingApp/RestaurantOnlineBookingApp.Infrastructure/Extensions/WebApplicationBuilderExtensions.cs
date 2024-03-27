
namespace RestaurantOnlineBookingApp.Infrastructure.Extensions
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using RestaurantOnlineBookingApp.Data.Models;
    using static RestaurantOnlineBookingApp.Common.ApplicationConstants;
    public static class WebApplicationBuilderExtensions
    {

        /// This method seeds admin role if it does not exist.
        /// Passed email should be valid email of existing user in the application.  

        //public static IApplicationBuilder SeedAdministrator(this IApplicationBuilder app, string email)
        //{
        //    using IServiceScope scopedServices = app.ApplicationServices.CreateScope();

        //    IServiceProvider serviceProvider = scopedServices.ServiceProvider;

        //    UserManager<AppUser> userManager =
        //        serviceProvider.GetRequiredService<UserManager<AppUser>>();
        //    RoleManager<IdentityRole<Guid>> roleManager =
        //        serviceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

        //    Task.Run(async () =>
        //    {
        //        if (await roleManager.RoleExistsAsync(AdminRoleName))
        //        {
        //            return;
        //        }

        //        IdentityRole<Guid> role =
        //            new IdentityRole<Guid>(AdminRoleName);

        //        await roleManager.CreateAsync(role);

        //        AppUser adminUser =
        //            await userManager.FindByEmailAsync(email);

        //        await userManager.AddToRoleAsync(adminUser, AdminRoleName);
        //    })
        //    .GetAwaiter()
        //    .GetResult();

        //    return app;
        //}
    }
}
