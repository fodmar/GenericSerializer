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

        public T Deserialize<T>(IDataSourceByKey dataSourceByKey)
        {
            return (T)this.Deserialize(typeof(T), dataSourceByKey, string.Empty);
        }

        private object Deserialize(Type type, IDataSourceByKey dataSourceByKey, string path)
        {
            DataSourceByKeyWrapper dataSourceByKeyWrapper = new DataSourceByKeyWrapper(dataSourceByKey, path);

            ConstructorInfoWrapper pickedConstructor = constructorSearcher.GetConstructorWithMostParametersThatCanSatisfy(type, dataSourceByKeyWrapper);

            object obj = pickedConstructor.Invoke();

            IDictionary<string, PropertyInfo> properties = type.GetSetters();

            foreach (KeyValuePair<string, PropertyInfo> pair in properties)
            {
                PropertyInfo property = pair.Value;

                if (property.PropertyType.IsClass && property.PropertyType != typeof(string)) // how to handle this?
                {
                    string propertyName = $"{path}.{property.Name}";

                    object nestedObj = this.Deserialize(property.PropertyType, dataSourceByKey, propertyName);

                    property.SetValue(obj, nestedObj);
                }
                else
                {
                    (bool hasKey, object value) = dataSourceByKeyWrapper.TryGetValueCaseInsensitive(pair.Key);
                    
                    if (hasKey)
                    {
                        property.SetValue(obj, value); 
                    }
                }
            }

            return obj;
        }

        public IDataSourceByKey Serialize<T>(T obj)
        {
            throw new NotImplementedException();
        }
    }
}
