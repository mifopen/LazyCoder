using System;
using Shouldly;

namespace LazyCoder.Tests
{
    public static class ShouldlyExtensions
    {
        public static void ShouldBeLines(this string actual, params string[] lines)
        {
            actual.ShouldBe(string.Join(Environment.NewLine, lines));
        }
    }
}