using System.Collections.Generic;
using System.Linq;

namespace GenericSerializer
{
    static class IDictionaryExtenstions
    {
        public static (bool, TValue) TryGetValue<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key)
        {
            bool hasValue = dict.TryGetValue(key, out TValue value);
            return (hasValue, value);
        }

        public static bool HasAnyKeyThatStartsWith<TValue>(this IDictionary<string, TValue> dict, string value)
        {
            return dict.Keys.Any(k => k.StartsWith(value));
        }
    }
}
