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

        public T Invoke<T>()
        {
            return (T)Constructor.Invoke(ParametersValues);
        }

        public bool TryMatchAndSetParameterValues(IDataSourceByKey dataSourceByKey)
        {
            object[] parameterValues = new object[ParameterCount];

            for (int i = 0; i < ParameterCount; i++)
            {
                ParameterInfo parameterInfo = Parameters[i];

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

                // parameter doesnt exists in datasource, so return false
                return false;
            }

            ParametersValues = parameterValues;
            return true;

        }
    }
}