using GenericSerializer;
using System;
using System.Collections.Generic;
using Xunit;

namespace GenericSerializerTests
{
    public class WithClassThatConstructorParameterIsClass : TestBase
    {
        private const string Param0Prop0 = "abc";
        private const int Param0Prop1 = 1234;
        private const string Param1Prop0 = "xyz";
        private const int Param1Prop1 = 123;

        public class Object
        {
            public Param Prop0 { get; set; }
            public Param Prop1 { get; set; }
            public Param Prop2 { get; set; }

            public Object(Param param0, Param param1)
            {
                throw new Exception("This constructor should not be callled");
            }

            public Object(Param param0, Param param1, Param param2 = null)
            {
                Prop0 = param0;
                Prop1 = param1;
                Prop2 = param2;
            }

            public Object(Param param0, Param param1, Param param2, Param param3 = null)
            {
                throw new Exception("This constructor should not be callled");
            }
        }

        public class Param
        {
            public string Prop0 { get; set; }
            public int Prop1 { get; set; }

            public Param(string param0, int param1 = Param1Prop1)
            {
                Prop0 = param0;
                Prop1 = param1;
            }
        }

        [Fact(DisplayName = "Object with class that constructor parameter is class")]
        public void TestWithClassProperty() => this.Test<Object>(PrepareData, Assert);

        private Dictionary<string, object> PrepareData => new Dictionary<string, object>
        {
            { "param0.param0", Param0Prop0 },
            { "param0.param1", Param0Prop1 },
            { "param1.param0", Param1Prop0 },
        };

        private void Assert(Object obj)
        {
            obj.Prop0.Prop0.ShouldEqual(Param0Prop0);
            obj.Prop0.Prop1.ShouldEqual(Param0Prop1);
            obj.Prop1.Prop0.ShouldEqual(Param1Prop0);
            obj.Prop1.Prop1.ShouldEqual(Param1Prop1);
        }
    }
}
