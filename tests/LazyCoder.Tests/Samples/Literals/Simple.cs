using System.Diagnostics.Contracts;
using System.Linq;
using Shouldly;
using Xunit;

namespace LazyCoder.Tests.Samples.Literals
{
    public class Simple
    {
        [Fact]
        public void Test()
        {
            var tsFiles = Converter.Convert(
                new[]
                {
                    typeof(SomeClass)
                },
                new[] { new DefaultCoder() });

            tsFiles.Select(x => x.Name)
                .ShouldBe(new[] { "SomeClass", "SomeEnum" },
                    ignoreOrder: true);

            Converter.WriteFileToString(tsFiles.Single(x => x.Name == "SomeClass"))
                .ShouldBeLines("import { SomeEnum } from \"./SomeEnum\";",
                    "",
                    "export interface SomeClass {",
                    "    EnumConst: SomeEnum.EnumValue2;",
                    "    BoolConst: true;",
                    "    StringConst: \"stringValue\";",
                    "    IntConst: 42;",
                    "}",
                    "");
        }

        private class SomeClass
        {
            public const SomeEnum EnumConst = SomeEnum.EnumValue2;
            public const bool BoolConst = true;
            public const string StringConst = "stringValue";
            public const int IntConst = 42;
        }

        private enum SomeEnum
        {
            EnumValue1,
            EnumValue2
        }

    }
}
