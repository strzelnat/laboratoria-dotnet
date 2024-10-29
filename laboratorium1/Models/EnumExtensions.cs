using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace laboratorium1.Models
{
    public static class EnumExtensions 
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .FirstOrDefault()?
                            .GetCustomAttribute<DisplayAttribute>()?
                            .GetName() ?? enumValue.ToString();
        }
    }
}
