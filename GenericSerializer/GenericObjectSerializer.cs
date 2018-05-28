using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GenericSerializer
{
    public class GenericObjectSerializer : IGenericObjectSerializer
    {
        public T Deserialize<T>(IDataSourceByKey dataSourceByKey)
        {
            Type type = typeof(T);

            IOrderedEnumerable<ConstructorInfo> constructors = type.GetConstructorsByParameterCount();
            Dictionary<string, object> readFromDataSource = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);

            ConstructorInfo pickedConstructor = null;
            object[] parameters = null;

            foreach (ConstructorInfo constructor in constructors)
            {
                parameters = GetParametersVales(constructor, readFromDataSource, dataSourceByKey);

                if (parameters != null)
                {
                    pickedConstructor = constructor;
                    break;
                }
            }

            T obj = (T)pickedConstructor.Invoke(parameters);

            IDictionary<string, PropertyInfo> properties = type.GetSetters();

            foreach (KeyValuePair<string, PropertyInfo> property in properties)
            {
                if (readFromDataSource.TryGetValue(property.Key, out object dataSourceValue))
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

        public object[] GetParametersVales(ConstructorInfo constructor, Dictionary<string, object> readDataSource, IDataSourceByKey dataSourceByKey)
        {
            ParameterInfo[] parameters = constructor.GetParameters();
            object[] parameterValues = new object[parameters.Length];

            for (int i = 0; i < parameters.Length; i++)
            {
                ParameterInfo parameterInfo = parameters[i];

                if (readDataSource.TryGetValue(parameterInfo.Name, out object value))
                {
                    parameterValues[i] = value;
                    continue;
                }

                (bool exisits, object dataSourceValue) = dataSourceByKey.TryGetValueCaseInsensitive(parameterInfo.Name);

                if (exisits)
                {
                    parameterValues[i] = dataSourceValue;
                    readDataSource.Add(parameterInfo.Name, dataSourceValue);
                    continue;
                }

                // parameter doesnt exists in datasource, so return null
                return null;
            }

            return parameterValues;
        }
    }
}
