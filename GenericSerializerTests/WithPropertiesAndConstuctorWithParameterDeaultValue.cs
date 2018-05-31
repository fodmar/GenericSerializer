using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GenericSerializerTests
{
    public class WithPropertiesAndConstuctorWithParameterDeaultValue : TestBase<WithPropertiesAndConstuctorWithParameterDeaultValue.Object>
    {
        private const string Prop0 = "abc";
        private const bool Prop1 = true;
        private const int Prop2 = 1234;

        public class Object
        {
            public Object(string prop0, bool prop1 = false, int prop2 = 1234)
            {
                Prop0 = prop0;
                Prop1 = prop1;
                Prop2 = prop2;
            }

            public string Prop0 { get; }
            public bool Prop1 { get; }
            public int Prop2 { get; }
        }

        [Fact(DisplayName = "Object with properties and constuctor with parameter deault value")]
        public void TestWithPropertiesAndConstuctorWithParameterDeaultValue() => this.Test();

        protected override Dictionary<string, object> PrepareData()
        {
            return new Dictionary<string, object>
            {
                { nameof(Object.Prop0), Prop0 },
                { nameof(Object.Prop1), Prop1 },
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
