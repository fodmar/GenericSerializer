using System;
using System.Collections.Generic;

namespace GenericSerializer
{
    public interface IGenericObjectSerializer
    {
        IDictionary<string, object> Serialize<T>(T obj);

        T Deserialize<T>(IDictionary<string, object> propertyValues);
    }
}
