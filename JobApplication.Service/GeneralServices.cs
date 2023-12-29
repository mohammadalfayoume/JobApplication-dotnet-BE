using JobApplication.Service.Services;
using System.ComponentModel.DataAnnotations;

namespace JobApplication.Service;

public static class GeneralServices
{
    public static string GetEnumDisplayName(Enum value)
    {
        var displayAttribute = (DisplayAttribute)Attribute.GetCustomAttribute(
            value.GetType().GetField(value.ToString()),
            typeof(DisplayAttribute));

        return displayAttribute?.Name ?? value.ToString();
    }
}
