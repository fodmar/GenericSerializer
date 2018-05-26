using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GenericSerializerTests
{
    public class WithPropertiesAndGetter : TestBase<WithPropertiesAndGetter.Object>
    {
        private const string Prop0 = "abc";
        private const bool Prop1 = true;
        private const int Prop2 = 1234;

        public class Object
        {
            public string Prop0 { get; set; }
            public bool Prop1 { get; set; }
            public int Prop2 { get; set; }
            public string Getter => Prop0;
        }

        [Fact(DisplayName = "Object with properties and getter")]
        public void TestWithPropertiesAndGetter() => this.Test();

        protected override Dictionary<string, object> PrepareData()
        {
            return new Dictionary<string, object>
            {
                { nameof(Object.Prop0), Prop0 },
                { nameof(Object.Prop1), Prop1 },
                { nameof(Object.Prop2), Prop2 },
            };
        }

        protected override void Assert(Object obj)
        {
            obj.Prop0.ShouldEqual(Prop0);
            obj.Prop1.ShouldEqual(Prop1);
            obj.Prop2.ShouldEqual(Prop2);
        }
    }
}
