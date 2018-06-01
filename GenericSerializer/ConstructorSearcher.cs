using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GenericSerializer
{
    class ConstructorSearcher
    {
        public ConstructorInfoWrapper GetConstructorWithMostParametersThatCanSatisfy(Type type, IDataSourceByKey dataSourceByKey)
        {
            IOrderedEnumerable<ConstructorInfoWrapper> constructors = type.GetConstructorsByParameterCount();

            foreach (ConstructorInfoWrapper constructor in constructors)
            {
                if (constructor.TryMatchAndSetParameterValues(dataSourceByKey))
                {
                    return constructor;
                }
            }

            return null;
        }
    }
}
