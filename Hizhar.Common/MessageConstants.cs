using System;

namespace Hizhar.Common
{
    public static class MessageConstants
    {
        public const string RequiredField = "Полето „{0}“ е задължително.";
        public const string LengthField = "Полето „{0}“ трябва да е между {2} и {1} символа.";
        public const string MaxLengthField = "Полето „{0}“ може да е най-много {1} символа.";
        public const string RangeField = "Полето „{0}“ трябва да е между {1} и {2}.";
        public const string SelectRequired = "Изберете стойност за „{0}“.";
        public const string InvalidUrl = "Въведете валиден адрес (URL).";
        public const string InvalidPhone = "Въведете валиден телефонен номер.";
        public const string DateInPast = "Датата не може да е в миналото.";
        public const string CheckOutBeforeCheckIn = "Датата на напускане трябва да е след датата на пристигане.";
        public const string InvalidFileExtension = "Позволени са само файлове: {0}.";
        public const string FileTooLarge = "Файлът е твърде голям (максимум {0} MB).";

        public const string SuccessMessageKey = "SuccessMessage";
        public const string ErrorMessageKey = "ErrorMessage";
    }
}
