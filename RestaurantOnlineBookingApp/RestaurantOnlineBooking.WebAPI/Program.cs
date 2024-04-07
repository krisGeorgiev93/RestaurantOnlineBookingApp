using Microsoft.EntityFrameworkCore;
using RestaurantOnlineBooking.Services.Data;
using RestaurantOnlineBooking.Services.Data.Interfaces;
using RestaurantOnlineBookingApp.Data;

namespace RestaurantOnlineBooking.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<RestaurantBookingDbContext>(options =>
            options.UseSqlServer(connectionString));

            builder.Services.AddScoped<IRestaurantService, RestaurantService>();
            builder.Services.AddScoped<IPhotoService, PhotoService>();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            builder.Services.AddCors(setup =>
            {
                setup.AddPolicy("RestaurantBookingOnlineApp", policyBuilder =>
                {
                    policyBuilder
                        .WithOrigins("https://localhost:7212")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.UseCors("RestaurantBookingOnlineApp");

            app.Run();
        }
    }
}
