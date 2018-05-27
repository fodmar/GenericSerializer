using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GenericSerializer
{
    public static class ReflectionHelper
    {
        public static IDictionary<string, PropertyInfo> GetSetters<T>()
        {
            return typeof(T)
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(p => p.CanWrite)
                .ToDictionary(p => p.Name);
        }
    }
}
