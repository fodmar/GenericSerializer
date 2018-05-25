using GenericSerializer;
using System;
using System.Collections.Generic;
using Xunit;

namespace GenericSerializerTests
{
    public class WithPropertiesOnly
    {
        class Object
        {
            public string Prop0 { get; set; }
            public bool Prop1 { get; set; }
            public int Prop2 { get; set; }
        }

        [Fact]
        public void Test1()
        {
            var dict = new Dictionary<string, object>
            {
                { nameof(Object.Prop0), "abc" },
                { nameof(Object.Prop1), true },
                { nameof(Object.Prop2), 1234 },
            };

            IDataSourceByKey dataSourceByKey = new DictionaryDataSourceByKey(dict);

            GenericObjectSerializer serializer = new GenericObjectSerializer();

            Object obj = serializer.Deserialize<Object>(dataSourceByKey);

            obj.Prop0.ShouldEqual(dict[nameof(Object.Prop0)]);
            obj.Prop1.ShouldEqual(dict[nameof(Object.Prop1)]);
            obj.Prop2.ShouldEqual(dict[nameof(Object.Prop2)]);
        }
    }
}
