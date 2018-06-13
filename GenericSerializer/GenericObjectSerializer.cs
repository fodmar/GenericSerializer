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

        public object Deserialize(Type type, IDictionary<string, object> propertyValues, string path)
        {
            ConstructorInfoWrapper pickedConstructor = type.GetConstructorWithMostParametersThatCanSatisfy(this, propertyValues, path);

            if (pickedConstructor == null)
            {
                return null;
            }

            object obj = pickedConstructor.Invoke();

            IEnumerable<PropertyInfo> setters = type.GetSetters();

            foreach (PropertyInfo setter in setters)
            {
                Type setterType = setter.PropertyType;
                string propertyPath = setter.Name.FormatPath(path);

                if (setterType.IsUserClass())
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
