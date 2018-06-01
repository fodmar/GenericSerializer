using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GenericSerializer
{
    static class TypeExtensions
    {
        public static IDictionary<string, PropertyInfo> GetSetters(this Type type)
        {
            return type
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(p => p.CanWrite)
                .ToDictionary(p => p.Name);
        }

        public static IOrderedEnumerable<ConstructorInfoWrapper> GetConstructorsByParameterCount(this Type type)
        {
            return type
                .GetConstructors()
                .Select(c => new ConstructorInfoWrapper(c))
                .OrderByDescending(c => c.ParameterCount);
        }
    }
}
