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

            IDataSourceByKey dataSourceByKey = new DictionaryDataSourceByKey(dict);

            GenericObjectSerializer serializer = new GenericObjectSerializer();

            T obj = serializer.Deserialize<T>(dataSourceByKey);

            Assert(obj);
        }
    }
}
