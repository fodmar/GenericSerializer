using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GenericSerializer
{
    class ConstructorSearcher
    {
        public (ConstructorInfo, object[]) GetConstructorWithMostParametersThatCanSatisfy(Type type, IDataSourceByKey dataSourceByKey)
        {
            IOrderedEnumerable<ConstructorInfo> constructors = type.GetConstructorsByParameterCount();

            ConstructorInfo pickedConstructor = null;
            object[] parameters = null;

            foreach (ConstructorInfo constructor in constructors)
            {
                parameters = GetParametersValues(constructor, dataSourceByKey);

                if (parameters != null)
                {
                    pickedConstructor = constructor;
                    break;
                }
            }

            return (pickedConstructor, parameters);
        }

        public object[] GetParametersValues(ConstructorInfo constructor, IDataSourceByKey dataSourceByKey)
        {
            ParameterInfo[] parameters = constructor.GetParameters();
            object[] parameterValues = new object[parameters.Length];

            for (int i = 0; i < parameters.Length; i++)
            {
                ParameterInfo parameterInfo = parameters[i];

                (bool exisits, object dataSourceValue) = dataSourceByKey.TryGetValueCaseInsensitive(parameterInfo.Name);

                if (exisits)
                {
                    parameterValues[i] = dataSourceValue;
                    continue;
                }
                else if (parameterInfo.HasDefaultValue)
                {
                    parameterValues[i] = parameterInfo.DefaultValue;
                    continue;
                }

                // parameter doesnt exists in datasource, so return null
                return null;
            }

            return parameterValues;
        }
    }
}
