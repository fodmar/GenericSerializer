using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GenericSerializer
{
    public class GenericObjectSerializer : IGenericObjectSerializer
    {
        private ConstructorSearcher constructorSearcher;

        public GenericObjectSerializer()
        {
            this.constructorSearcher = new ConstructorSearcher();
        }

        public T Deserialize<T>(IDictionary<string, object> propertyValues)
        {
            return (T)this.Deserialize(typeof(T), propertyValues, string.Empty);
        }

        private object Deserialize(Type type, IDictionary<string, object> propertyValues, string path)
        {
            ConstructorInfoWrapper pickedConstructor = constructorSearcher.GetConstructorWithMostParametersThatCanSatisfy(type, propertyValues);

            object obj = pickedConstructor.Invoke();

            IDictionary<string, PropertyInfo> properties = type.GetSetters();

            foreach (KeyValuePair<string, PropertyInfo> pair in properties)
            {
                PropertyInfo property = pair.Value;
                string propertyPath = property.Name.FormatPath(path);

                if (property.PropertyType.IsClass && property.PropertyType != typeof(string)) // how to handle this?
                {
                    object nestedObj = this.Deserialize(property.PropertyType, propertyValues, propertyPath);

                    property.SetValue(obj, nestedObj);
                }
                else
                {
                    (bool hasKey, object value) = propertyValues.TryGetValue(propertyPath);
                    
                    if (hasKey)
                    {
                        property.SetValue(obj, value); 
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
