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
            public const int NameMinLength = 2;
            public const int NameMaxLength = 100;
        }

        public static class ApplicationUser
        {

        }

        public static class Meal
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 50;
            public const int DescriptionMinLength = 5;
            public const int DescriptionMaxLength = 500;
            public const string PriceMinValue = "0";
            public const string PriceMaxValue = "6000";
        }

        public static class Owner
        {
            public const int PhoneNumberMinLength = 6;
            public const int PhoneNumberMaxLength = 14;
        }

        public static class Review
        {
            public const int CommentMinLength = 2;
            public const int CommentMaxLength = 500;
            public const int ReviewRatingMinValue = 1;
            public const int ReviewRatingMaxValue = 10;
        }

        public static class Event
        {
            public const int TitleMinLength = 2;
            public const int TitleMaxLength = 100;
            public const int DescriptionMinLength = 5;
            public const int DescriptionMaxLength = 500;
            public const string MinPriceValue = "1";
            public const string MaxPriceValue = "1000";
        }

    }
}
