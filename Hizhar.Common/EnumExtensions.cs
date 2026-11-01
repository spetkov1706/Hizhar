using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Hizhar.Common
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum value)
        {
            MemberInfo? member = value.GetType().GetMember(value.ToString()).FirstOrDefault();
            DisplayAttribute? display = member?.GetCustomAttribute<DisplayAttribute>();

            return display?.GetName() ?? value.ToString();
        }
    }
}
