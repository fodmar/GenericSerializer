using GenericSerializer;
using System;
using System.Collections.Generic;
using Xunit;

namespace GenericSerializerTests
{
    public class WithPropertiesOnly : TestBase
    {
        private const string Prop0 = "abc";
        private const bool Prop1 = true;
        private const int Prop2 = 1234;

        public class Object
        {
            public string Prop0 { get; set; }
            public bool Prop1 { get; set; }
            public int Prop2 { get; set; }
        }

        [Fact(DisplayName = "Object with properties only")]
        public void TestWithPropertiesOnly() => this.Test<Object>(PrepareData, Assert);

        private Dictionary<string, object> PrepareData => new Dictionary<string, object>
        {
            { nameof(Object.Prop0), Prop0 },
            { nameof(Object.Prop1), Prop1 },
            { nameof(Object.Prop2), Prop2 },
        };

        private void Assert(Object obj)
        {
            obj.Prop0.ShouldEqual(Prop0);
            obj.Prop1.ShouldEqual(Prop1);
            obj.Prop2.ShouldEqual(Prop2);
        }
    }
}
