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

        public static void ShouldNotBeNull<T>(this T current) where T : class
        {
            Assert.NotNull(current);
        }

        public static void ShouldBeNull<T>(this T current) where T : class
        {
            Assert.Null(current);
        }
    }
}
