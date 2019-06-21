using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Egil.RazorComponents.Bootstrap.Extensions
{
    public static class DictionaryExtensions
    {
        public static bool TryGetValue<TKey, TValue>(this IReadOnlyDictionary<TKey, object> dictionary, TKey key, out TValue value)
        {
            value = default;

            if(dictionary.TryGetValue(key, out object objectValue) && objectValue is TValue requestedValue)
            {
                value = requestedValue;
                return true;
            }
            return false;
        }
    }
}
