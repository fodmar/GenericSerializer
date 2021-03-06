﻿using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GenericSerializerTests
{
    public class WithPropertiesAndConstructor : TestBase
    {
        private const string param0 = "abc";
        private const bool param1 = true;
        private const int Prop2 = 1234;

        public class Object
        {
            public Object(string param0, bool param1)
            {
                Prop0 = param0;
                Prop1 = param1;
            }

            public string Prop0 { get; }
            public bool Prop1 { get; }
            public int Prop2 { get; set; }
        }

        [Fact(DisplayName = "Object with properties and constructor")]
        public void TestWithPropertiesAndConstructor() => this.Test<Object>(PrepareData, Assert);

        private Dictionary<string, object> PrepareData => new Dictionary<string, object>
        {
            { nameof(param0), param0 },
            { nameof(param1), param1 },
            { nameof(Object.Prop2), Prop2 },
        };

        private void Assert(Object obj)
        {
            obj.Prop0.ShouldEqual(param0);
            obj.Prop1.ShouldEqual(param1);
            obj.Prop2.ShouldEqual(Prop2);
        }
    }
}
