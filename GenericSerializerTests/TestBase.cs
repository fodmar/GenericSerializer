using GenericSerializer;
using System;
using System.Collections.Generic;

namespace GenericSerializerTests
{
    public abstract class TestBase
    {
        protected void Test<T>(Dictionary<string, object> dict, Action<T> assert)
        {
            GenericObjectSerializer serializer = new GenericObjectSerializer();

            T obj = serializer.Deserialize<T>(dict);

            assert(obj);
        }
    }
}
