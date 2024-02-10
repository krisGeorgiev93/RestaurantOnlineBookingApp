
namespace RestaurantOnlineBookingApp.Web
{
    using Microsoft.AspNetCore.Identity;

    using Microsoft.EntityFrameworkCore;

    using RestaurantOnlineBookingApp.Data;
    using RestaurantOnlineBookingApp.Data.Models;

    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            string connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<RestaurantBookingDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddDefaultIdentity<AppUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;

            })
             .AddRoles<IdentityRole<Guid>>()
             .AddEntityFrameworkStores<RestaurantBookingDbContext>();

            builder.Services.AddControllersWithViews();

            WebApplication app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapDefaultControllerRoute();
            app.MapRazorPages();

            app.Run();
        }
    }
}