using System;
using System.Collections.Generic;
using System.Reflection;

namespace Egil.RazorComponents.Bootstrap.Extensions
{
    public static class ReflectionExtensions
    {
        public static PropertyInfo[] GetInstanceProperties(this Type sourceType)
        {
            return sourceType.GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
        }

        public static IEnumerable<PropertyInfo> IsAssignableFrom(this IEnumerable<PropertyInfo> properties, Type assignableFromType)
        {
            foreach (var prop in properties)
            {
                if (assignableFromType.IsAssignableFrom(prop.PropertyType))
                    yield return prop;
            }
        }

        public static T GetValue<T>(this PropertyInfo property, object sourceObject)
        {
            return (T)property.GetValue(sourceObject);
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