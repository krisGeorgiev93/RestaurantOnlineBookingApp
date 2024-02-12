using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantOnlineBookingApp.Common
{
    public static class ValidationConstants
    {

        public static class Restaurant
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 60;

            public const int AddressMinLength = 5;
            public const int AddressMaxLength = 140;

            public const int DescriptionMinLength = 10;
            public const int DescriptionMaxLength = 150;

            public const int ImageUrlMaxLength = 2000;



        }

        public static class Category
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 60;
        }

        public static class City
        {

        }

        public static class ApplicationUser
        {

        }

        public static class Menu
        {

        }

        public static class Owner
        {
            public const int PhoneNumberMinLength = 6;
            public const int PhoneNumberMaxLength = 14;



        }

    }
}
