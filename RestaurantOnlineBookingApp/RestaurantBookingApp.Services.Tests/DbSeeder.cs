using RestaurantOnlineBookingApp.Data;
using RestaurantOnlineBookingApp.Data.Models;

namespace RestaurantBookingApp.Services.Tests
{
    public class DbSeeder
    {
        public static Owner Owner;
        public static AppUser OwnerUser;
        public static AppUser GuestUser;

        public static void SeedDatabase(RestaurantBookingDbContext dbContext)
        {
            OwnerUser = new AppUser()
            {
                UserName = "Ivan",
                NormalizedUserName = "IVAN",
                Email = "ivan@mail.com",
                NormalizedEmail = "IVAN@MAIL.COM",
                EmailConfirmed = true,
                PasswordHash = "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92",
                SecurityStamp = "7607f303-5f45-48a3-81b9-59e41c1ee75e",
                TwoFactorEnabled = false,
                FirstName = "Ivan",
                LastName = "Ivanov"
            };

            GuestUser = new AppUser()
            {
                UserName = "Kiro",
                NormalizedUserName = "KIRO",
                Email = "kiro@mail.com",
                NormalizedEmail = "KIRO@MAIL.COM",
                EmailConfirmed = true,
                PasswordHash = "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92",
                SecurityStamp = "1eb9bc80-b855-4bf0-a243-b82a2733ed75",
                TwoFactorEnabled = false,
                FirstName = "Kiro",
                LastName = "Petkov"
            };
            Owner = new Owner()
            {
                PhoneNumber = "+359767672654",
                User = OwnerUser,
            };

            dbContext.Users.Add(OwnerUser);
            dbContext.Users.Add(GuestUser);
            dbContext.Owners.Add(Owner);

            dbContext.SaveChanges();

        }
    }
}
