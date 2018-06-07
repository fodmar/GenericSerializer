using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GenericSerializer
{
    public class GenericObjectSerializer : IGenericObjectSerializer
    {
        public T Deserialize<T>(IDictionary<string, object> propertyValues)
        {
            return (T)this.Deserialize(typeof(T), propertyValues, string.Empty);
        }

        private object Deserialize(Type type, IDictionary<string, object> propertyValues, string path)
        {
            ConstructorInfoWrapper pickedConstructor = type.GetConstructorWithMostParametersThatCanSatisfy(propertyValues);

            object obj = pickedConstructor.Invoke();

            IEnumerable<PropertyInfo> setters = type.GetSetters();

            foreach (PropertyInfo setter in setters)
            {
                Type setterType = setter.PropertyType;
                string propertyPath = setter.Name.FormatPath(path);

                if (setterType.IsClass && setterType != typeof(string)) // how to handle this?
                {
                    if (propertyValues.HasAnyKeyThatStartsWith(propertyPath))
                    {
                        object nestedObj = this.Deserialize(setterType, propertyValues, propertyPath);

                        setter.SetValue(obj, nestedObj);
                    }
                }
                else
                {
                    (bool hasKey, object value) = propertyValues.TryGetValue(propertyPath);
                    
                    if (hasKey)
                    {
                        setter.SetValue(obj, value); 
                    }
                }
            }

            return obj;
        }

        public IDictionary<string, object> Serialize<T>(T obj)
        {
            throw new NotImplementedException();
        }
    }
}
