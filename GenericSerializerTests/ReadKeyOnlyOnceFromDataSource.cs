using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GenericSerializerTests
{
    public class ReadKeyOnlyOnceFromDataSource : TestBase<ReadKeyOnlyOnceFromDataSource.Object>
    {
        private const string Prop0 = "abc";

        public class Object
        {
            public string Prop0 { get; set; }

            public Object(string prop0)
            {
            }

            public Object(string prop0, string prop1)
            {
            }

            public Object(string prop0, string prop1, string prop2)
            {
            }
        }

        [Fact(DisplayName = "Read key only once from data source")]
        public void TestReadKeyOnlyOnceFromDataSource() => this.Test();

        protected override Dictionary<string, object> PrepareData()
        {
            return new Dictionary<string, object>
            {
                { nameof(Object.Prop0), Prop0 }
            };
        }

        protected override void Assert(Object obj)
        {
            obj.Prop0.ShouldEqual(Prop0);
        }
    }
}
