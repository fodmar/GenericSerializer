using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GenericSerializerTests
{
    public class ReadKeyOnlyOnceFromDataSource : TestBase<ReadKeyOnlyOnceFromDataSource.Object>
    {
        private const string param0 = "abc";

        public class Object
        {
            public string Prop0 { get; set; }

            public Object(string param0)
            {
                Prop0 = param0;
            }

            public Object(string param0, string param1)
            {
            }

            public Object(string param0, string param1, string param2)
            {
            }
        }

        [Fact(DisplayName = "Read key only once from data source")]
        public void TestReadKeyOnlyOnceFromDataSource() => this.Test();

        protected override Dictionary<string, object> PrepareData()
        {
            return new Dictionary<string, object>
            {
                { nameof(param0), param0 }
            };
        }

        protected override void Assert(Object obj)
        {
            obj.Prop0.ShouldEqual(param0);
        }
    }
}
