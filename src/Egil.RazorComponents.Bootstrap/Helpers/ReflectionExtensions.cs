using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Egil.RazorComponents.Bootstrap.Helpers
{
public static class ReflectionExtensions
{
    public static IEnumerable<PropertyInfo> GetPropertiesAssignableFrom<T>(this Type sourceType)
    {
        var assignableFromType = typeof(T);
        var properties = sourceType.GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
        foreach (var prop in properties)
        {
            if (prop.DeclaringType == sourceType && assignableFromType.IsAssignableFrom(prop.PropertyType))
                yield return prop;
        }
    }

    public static IEnumerable<T> GetValues<T>(this IEnumerable<PropertyInfo> properties, object sourceObject) where T : class
    {
        foreach (var propInfo in properties)
        {
            var value = propInfo.GetValue(sourceObject) as T;
            if (value is null) continue;
            yield return value;
        }
    }
}
}
