using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantOnlineBookingApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                Id = Guid.NewGuid(),
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
                Id = Guid.NewGuid(),
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
