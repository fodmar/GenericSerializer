using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GenericSerializer
{
    static class TypeExtensions
    {
        public static IEnumerable<PropertyInfo> GetSetters(this Type type)
        {
            return type
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(p => p.CanWrite);
        }

        public static IOrderedEnumerable<ConstructorInfoWrapper> GetConstructorsByParameterCount(this Type type)
        {
            return type
                .GetConstructors()
                .Select(c => new ConstructorInfoWrapper(c))
                .OrderByDescending(c => c.ParameterCount);
        }

        public static ConstructorInfoWrapper GetConstructorWithMostParametersThatCanSatisfy(this Type type, IDictionary<string, object> propertyValues)
        {
            return type
                .GetConstructorsByParameterCount()
                .FirstOrDefault(c => c.TryMatchAndSetParameterValues(propertyValues));
        }
    }
}
