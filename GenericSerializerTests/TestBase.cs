using GenericSerializer;
using System.Collections.Generic;

namespace GenericSerializerTests
{
    public abstract class TestBase<T>
    {
        protected abstract Dictionary<string, object> PrepareData();
        protected abstract void Assert(T obj);

        protected void Test()
        {
            Dictionary<string, object> dict = PrepareData();

            GenericObjectSerializer serializer = new GenericObjectSerializer();

            T obj = serializer.Deserialize<T>(dict);

            Assert(obj);
        }
    }
}
