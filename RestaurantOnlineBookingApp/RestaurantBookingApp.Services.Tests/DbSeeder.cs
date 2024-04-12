namespace RestaurantBookingApp.Services.Tests
{
    using RestaurantOnlineBookingApp.Data;
    using RestaurantOnlineBookingApp.Data.Models;
    public class DbSeeder
    {
        public static Owner Owner1;
        public static Owner Owner2;

        public static AppUser OwnerUser1;
        public static AppUser OwnerUser2;

        public static AppUser GuestUser;

        public static Restaurant Restaurant;
        public static Review Review;
        public static Event Event;
        public static Meal Meal1;
        
        public static void SeedDatabase(RestaurantBookingDbContext dbContext)
        {
            OwnerUser1 = new AppUser()
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

            OwnerUser2 = new AppUser()
            {
                UserName = "Hristo",
                NormalizedUserName = "Hristov",
                Email = "hristo@mail.com",
                NormalizedEmail = "HRISTO@MAIL.COM",
                EmailConfirmed = true,
                PasswordHash = "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92",
                SecurityStamp = "9403b0fd-c20a-4eb3-9307-fb8289a49676",
                TwoFactorEnabled = false,
                FirstName = "Hristo",
                LastName = "Hristov"
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

            Owner1 = new Owner()
            {
                PhoneNumber = "+359767672654",
                UserId = OwnerUser1.Id,
                User = OwnerUser1,

            };

            Owner2 = new Owner()
            {
                PhoneNumber = "+359765625455",
                UserId = OwnerUser2.Id,
                User = OwnerUser2,
            };          

            dbContext.Users.Add(OwnerUser1);
            dbContext.Users.Add(OwnerUser2);
            dbContext.Users.Add(GuestUser);

            dbContext.Owners.Add(Owner1);
            dbContext.Owners.Add(Owner2);

            Restaurant = new Restaurant()
            {
                Name = "Restaurant1",
                Address = "Ivan Ivanov 22",
                Description = "Family restaurant with italian food",
                StartingTime = new TimeSpan(12, 0, 0),
                EndingTime = new TimeSpan(23, 45, 0),
                ImageUrl = "ImageUrl",
                Capacity = 100,
                IsActive = true,
                CityId = 2,
                CategoryId = 1,
                Owner = Owner1,
                OwnerId = Owner1.Id
            };

            Review = new Review
            {
                ReviewRating = 10, 
                Comment = "Great experience", 
                GuestId = GuestUser.Id,
                RestaurantId = Restaurant.Id, 
                CreatedAt = DateTime.Now 
            };

            Event = new Event
            {
                RestaurantId = Restaurant.Id,
                Title = "Test Event",
                Date = DateTime.Today,
                Time = new TimeSpan(18, 0, 0),
                Description = "Test event description",
                Price = 50.00m,
                ImageUrl = "EventImageUrl"
            };

            Meal1 = new Meal
            {
                Id = 1,
                Name = "Spaghetti Carbonara",
                Description = "Pasta with egg, hard cheese, guanciale, and pepper.",
                Price = 12.99m,
                ImageUrl = "imageUrl",
                RestaurantId = Guid.NewGuid()
            };

            City city1 = new City
            {
                CityName = "Sofia", 
            };
            dbContext.Cities.Add(city1);
            City city2 = new City
            {
                CityName = "Plovdiv",
            };
            dbContext.Cities.Add(city2);

            dbContext.Events.Add(Event);
            dbContext.Reviews.Add(Review);           
            dbContext.Restaurants.Add(Restaurant);           
            
            dbContext.SaveChanges();

        }
    }
}
