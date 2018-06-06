using System.Collections.Generic;

namespace GenericSerializer
{
    static class IDictionaryExtenstions
    {
        public static (bool, TValue) TryGetValue<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key)
        {
            TValue value;
            bool hasValue = dict.TryGetValue(key, out value);
            return (hasValue, value);
        }
    }
}
