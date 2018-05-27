using System;
using System.Collections.Generic;
using System.Reflection;

namespace GenericSerializer
{
    public class GenericObjectSerializer : IGenericObjectSerializer
    {
        public T Deserialize<T>(IDataSourceByKey dataSourceByKey)
        {
            T obj = Activator.CreateInstance<T>();

            IDictionary<string, PropertyInfo> properties = ReflectionHelper.GetSetters<T>();

            foreach (KeyValuePair<string, PropertyInfo> property in properties)
            {
                property.Value.SetValue(obj, dataSourceByKey[property.Key]);
            }

            return obj;
        }

        public IDataSourceByKey Serialize<T>(T obj)
        {
            throw new System.NotImplementedException();
        }
    }
}
