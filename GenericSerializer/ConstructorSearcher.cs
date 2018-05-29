using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GenericSerializer
{
    class ConstructorSearcher
    {
        public (ConstructorInfo, object[]) GetConstructorWithMostParametersThatCanSatisfy(Type type, IDataSourceByKey dataSourceByKey, Dictionary<string, object> dataSourceCache)
        {
            IOrderedEnumerable<ConstructorInfo> constructors = type.GetConstructorsByParameterCount();

            ConstructorInfo pickedConstructor = null;
            object[] parameters = null;

            foreach (ConstructorInfo constructor in constructors)
            {
                parameters = GetParametersValues(constructor, dataSourceByKey, dataSourceCache);

                if (parameters != null)
                {
                    pickedConstructor = constructor;
                    break;
                }
            }

            return (pickedConstructor, parameters);
        }

        public object[] GetParametersValues(ConstructorInfo constructor, IDataSourceByKey dataSourceByKey, Dictionary<string, object> dataSourceCache)
        {
            ParameterInfo[] parameters = constructor.GetParameters();
            object[] parameterValues = new object[parameters.Length];

            for (int i = 0; i < parameters.Length; i++)
            {
                ParameterInfo parameterInfo = parameters[i];

                if (dataSourceCache.TryGetValue(parameterInfo.Name, out object value))
                {
                    parameterValues[i] = value;
                    continue;
                }

                (bool exisits, object dataSourceValue) = dataSourceByKey.TryGetValueCaseInsensitive(parameterInfo.Name);

                if (exisits)
                {
                    parameterValues[i] = dataSourceValue;
                    dataSourceCache.Add(parameterInfo.Name, dataSourceValue);
                    continue;
                }

                // parameter doesnt exists in datasource, so return null
                return null;
            }

            return parameterValues;
        }
    }
}
