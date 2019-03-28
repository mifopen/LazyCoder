using Shouldly;
using Xunit;

namespace LazyCoder.Tests
{
    public class HelpersTest
    {
        [Fact]
        public void PathFromAToB()
        {
            Helpers.GetPathFromAToB(new[] { "a", "b", "c", "d" },
                                    new[] { "a", "b" })
                   .ShouldBe("../..");

            Helpers.GetPathFromAToB(new[] { "a", "b", "c", "d" },
                                    new[] { "f", "g" })
                   .ShouldBe("../../../../f/g");

            Helpers.GetPathFromAToB(new[] { "a", "b", "c", "d" },
                                    new[] { "a", "b", "c", "d", "e", "f" })
                   .ShouldBe("./e/f");

            Helpers.GetPathFromAToB(new[] { "a", "b", "c", "d" },
                                    new[] { "a", "b", "c", "d" })
                   .ShouldBe(".");

            Helpers.GetPathFromAToB(new[] { "a", "b", "c", "d" },
                                    new[] { "a", "b", "e" })
                   .ShouldBe("../../e");

            Helpers.GetPathFromAToB(new[] { "a", "b", "c", "d" },
                                    new[] { "..", "a" })
                   .ShouldBe("../../../../../a");
        }
    }
}
