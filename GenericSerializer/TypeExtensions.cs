using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GenericSerializer
{
    public static class TypeExtensions
    {
        public static IDictionary<string, PropertyInfo> GetSetters(this Type type)
        {
            return type
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(p => p.CanWrite)
                .ToDictionary(p => p.Name);
        }

        public static IOrderedEnumerable<ConstructorInfo> GetConstructorsByParameterCount(this Type type)
        {
            return type
                .GetConstructors()
                .OrderByDescending(c => c.GetParameters().Length);
        }
    }
}
