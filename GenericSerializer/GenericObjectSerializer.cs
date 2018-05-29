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
            Type type = typeof(T);
            Dictionary<string, object> dataSourceCache = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);

            (ConstructorInfo pickedConstructor, object[] parameters) = constructorSearcher.GetConstructorWithMostParametersThatCanSatisfy(type, dataSourceByKey, dataSourceCache);

            T obj = (T)pickedConstructor.Invoke(parameters);

            IDictionary<string, PropertyInfo> properties = type.GetSetters();

            foreach (KeyValuePair<string, PropertyInfo> property in properties)
            {
                if (dataSourceCache.TryGetValue(property.Key, out object dataSourceValue))
                {
                    property.Value.SetValue(obj, dataSourceValue);
                    continue;
                }

                (bool hasKey, object value) = dataSourceByKey.TryGetValueCaseInsensitive(property.Key);

                if (hasKey)
                {
                    property.Value.SetValue(obj, value); 
                }
            }

            return obj;
        }

        public IDataSourceByKey Serialize<T>(T obj)
        {
            throw new System.NotImplementedException();
        }
    }
}
