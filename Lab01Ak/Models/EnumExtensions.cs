using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Lab01Ak.Models;

namespace LaboratoriumASPNET.Models;

public static class EnumExtensions
{
    public static string GetDisplayName(this Category enumValue)
    {
        return enumValue.GetType()
            .GetMember(enumValue.ToString())
            .First()
            .GetCustomAttribute<DisplayAttribute>()
            .GetName();
    }
}