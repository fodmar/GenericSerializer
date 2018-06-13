using System.Collections.Generic;
using System.Reflection;

namespace GenericSerializer
{
    class ConstructorInfoWrapper
    {
        public ConstructorInfoWrapper(ConstructorInfo constructorInfo)
        {
            this.Constructor = constructorInfo;
            this.Parameters = constructorInfo.GetParameters();
        }

        public ConstructorInfo Constructor { get; private set; }

        public ParameterInfo[] Parameters { get; private set; }

        public object[] ParametersValues { get; private set; }

        public int ParameterCount => Parameters.Length;

        public object Invoke()
        {
            return Constructor.Invoke(ParametersValues);
        }

        public bool TryMatchAndSetParameterValues(GenericObjectSerializer genericSerializer, IDictionary<string, object> propertyValues, string path)
        {
            object[] parameterValues = new object[ParameterCount];

            for (int i = 0; i < ParameterCount; i++)
            {
                ParameterInfo parameterInfo = Parameters[i];
                string parameterName = parameterInfo.Name.FormatPath(path);

                if (parameterInfo.ParameterType.IsUserClass())
                {
                    parameterValues[i] = genericSerializer.Deserialize(parameterInfo.ParameterType, propertyValues, parameterName);

                    if (parameterValues[i] == null)
                    {
                        if (parameterInfo.HasDefaultValue)
                        {
                            parameterValues[i] = parameterInfo.DefaultValue;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    (bool exisits, object dataSourceValue) = propertyValues.TryGetValue(parameterName);

                    if (exisits)
                    {
                        parameterValues[i] = dataSourceValue;
                    }
                    else if (parameterInfo.HasDefaultValue)
                    {
                        parameterValues[i] = parameterInfo.DefaultValue;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            ParametersValues = parameterValues;
            return true;
        }
    }
}