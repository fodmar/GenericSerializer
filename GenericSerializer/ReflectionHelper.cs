using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GenericSerializer
{
    public static class ReflectionHelper
    {
        public static IDictionary<string, PropertyInfo> GetProperties<T>()
        {
            return typeof(T)
                .GetProperties(BindingFlags.Static)
                .ToDictionary(p => p.Name);
        }
    }
}
