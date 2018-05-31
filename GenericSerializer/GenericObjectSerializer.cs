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
            DataSourceByKeyWrapper dataSourceByKeyWrapper = new DataSourceByKeyWrapper(dataSourceByKey);

            (ConstructorInfo pickedConstructor, object[] parameters) = constructorSearcher.GetConstructorWithMostParametersThatCanSatisfy(type, dataSourceByKeyWrapper);

            T obj = (T)pickedConstructor.Invoke(parameters);

            IDictionary<string, PropertyInfo> properties = type.GetSetters();

            foreach (KeyValuePair<string, PropertyInfo> property in properties)
            {
                (bool hasKey, object value) = dataSourceByKeyWrapper.TryGetValueCaseInsensitive(property.Key);

                if (hasKey)
                {
                    property.Value.SetValue(obj, value); 
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
