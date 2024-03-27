using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantOnlineBookingApp.Data.Models;

namespace RestaurantOnlineBookingApp.Data.Configurations
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasData(SeedUsers());
        }

        private List<AppUser> SeedUsers() 
        {
            AppUser user;
            var users = new List<AppUser>();
            var hasher = new PasswordHasher<AppUser>();

             user = new AppUser()
            {
                Id = Guid.Parse("faf6dc41-ce01-44a9-b63c-0abd2df2d15f"),
                FirstName = "Kiril",
                LastName = "Ivanov",
                UserName = "kiril@mail.com",
                NormalizedUserName = "kiril@mail.com",
                Email = "kiril@mail.com",
                NormalizedEmail = "kiril@mail.com"
            };
            user.PasswordHash = hasher.HashPassword(user, "123456");
            users.Add(user);

             user = new AppUser()
            {
                Id = Guid.Parse("8099b56d-7710-415d-9c06-4569082c6758"),
                FirstName = "Ivan",
                LastName = "Georgiev",
                UserName = "ivan@mail.com",
                NormalizedUserName = "ivan@mail.com",
                Email = "ivan@mail.com",
                NormalizedEmail = "ivan@mail.com"
            };
            user.PasswordHash = hasher.HashPassword(user, "123456");
            users.Add(user);

            return users;
        }
    }
}
