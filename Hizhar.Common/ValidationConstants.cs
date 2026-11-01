using System;

namespace Hizhar.Common
{
    public static class ValidationConstants
    {
        public static class Mountain 
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 50;
            public const int DescriptionMinLength = 10;
            public const int DescriptionMaxLength = 2000;
            public const int ImageUrlMaxLength = 2048;
        }

        public static class Hut
        {
            public const int NameMinLength = 3;
            public const int NameMaxLength = 80;
            public const int DescriptionMinLength = 10;
            public const int DescriprionMaxLength = 3000;
            public const int AltitudeMin = 0;
            public const int AltitudeMax = 30000;
            public const int MedsMin = 1;
            public const int BedsMax = 500;
            public const string PriceMin = "0";
            public const string PriceMax = "500";
            public const int PhoneNumberMaxLength = 20;
            public const string PhoneNumberRegex = @"^\+?[0-9 ]{6, 20}";
            public const int ImageUrlMaxLength = 2048;
        }
        public static class Peak
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 60;
            public const int ElevationMin = 0;
            public const int ElevationMax = 3000;
            public const int DescriptionMaxLength = 2000;
        }

        public static class Trail
        {
            public const int NameMinLength = 3;
            public const int NameMaxLength = 100;
            public const int DescriptionMinLength = 10;
            public const int DescriprionMaxLength = 3000;
            public const double LengthMin = 0.1;
            public const double LengthMax = 100;
            public const int ElevationGainMin = 0;
            public const int ElevationGainMax = 3000;
            public const int DurationMin = 10;
            public const int DurationMax = 2880;
            public const int PointsTextMaxLength = 4000;

        }

        public static class TrailPoint
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 80;
        }

        public static class Coordinates
        {
            public const double LatitudeMin = 41.0;
            public const double LatitudeMax = 44.5;
            public const double LongitudeMin = 22.0;
            public const double LongitudeMax = 29.0;
        }

        public static class Reservation
        {
            public const int GuestsMin = 1;
            public const int GuestsMax = 30;
            public const int NoteMaxLength = 500;
            public const int MaxNights = 14;
        }

        public static class Review
        {
            public const int RatingMin = 1;
            public const int RatingMax = 5;
            public const int ContentMinLength = 10;
            public const int ContentMaxLength = 1000;
        }

        public static class Photo
        {
            public const int CaptionMaxLength = 200;
            public const int FilePathMaxLength = 300;
            public const int MaxFileSizeInBytes = 5 * 1024 * 1024;
            public const string AllowedExtensions = ".jpg,.jpeg,.png,.webp";
        }

        public static class TrailCondition
        {
            public const int DescriptionMinLength = 5;
            public const int DescriptionMaxLength = 1000;
        }

        public static class HutKeeperRequest
        {
            public const int MessageMinLength = 10;
            public const int MessageMaxLength = 1000;
        }
    }
}
