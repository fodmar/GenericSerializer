using GenericSerializer;
using System;
using System.Collections.Generic;
using Xunit;

namespace GenericSerializerTests
{
    public class WithClassProperty : TestBase<WithClassProperty.Object>
    {
        private const string Prop0 = "abc";
        private const bool Prop1 = true;
        private const int Prop2 = 1234;
        private const string NestedProp0 = "abc";
        private const bool NestedProp1 = true;
        private const int NestedProp2 = 1234;

        public class Object
        {
            public string Prop0 { get; set; }
            public bool Prop1 { get; set; }
            public int Prop2 { get; set; }
            //public Object Nested { get; set; }
        }

        [Fact(DisplayName = "Object with class property")]
        public void TestWithClassProperty() => this.Test();

        protected override Dictionary<string, object> PrepareData()
        {
            return new Dictionary<string, object>
            {
                { nameof(Object.Prop0), Prop0 },
                { nameof(Object.Prop1), Prop1 },
                { nameof(Object.Prop2), Prop2 },
                //{ $"{nameof(Object.Nested)}.{nameof(Object.Prop0)}", NestedProp0 },
                //{ $"{nameof(Object.Nested)}.{nameof(Object.Prop1)}", NestedProp1 },
                //{ $"{nameof(Object.Nested)}.{nameof(Object.Prop2)}", NestedProp2 },
            };
        }

        protected override void Assert(Object obj)
        {
            obj.Prop0.ShouldEqual(Prop0);
            obj.Prop1.ShouldEqual(Prop1);
            obj.Prop2.ShouldEqual(Prop2);
            //obj.Nested.ShouldNotBeNull();
            //obj.Nested.Prop0.ShouldEqual(NestedProp0);
            //obj.Nested.Prop1.ShouldEqual(NestedProp1);
            //obj.Nested.Prop2.ShouldEqual(NestedProp2);
            //obj.Nested.Nested.ShouldBeNull();
        }
    }
}
