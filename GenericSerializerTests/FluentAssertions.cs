using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GenericSerializerTests
{
    public static class FluentAssertions
    {
        public static void ShouldEqual<T>(this T current, T expected)
        {
            Assert.Equal(expected, current);
        }
    }
}
