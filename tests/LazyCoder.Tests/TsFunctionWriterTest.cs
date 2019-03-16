using System;
using LazyCoder.Typescript;
using Xunit;

namespace LazyCoder.Tests
{
    public class TsFunctionWriterTest
    {
        [Fact]
        public void Simple()
        {
            var tsName = new TsFunction
                         {
                             Name = "SomeFunction",
                             Parameters = new[]
                                          {
                                              new TsFunctionParameter { Name = "firstParameter", Type = new TsTypeReference("FirstType") },
                                              new TsFunctionParameter { Name = "secondParameter", Type = new TsTypeReference("SecondType") }
                                          },
                             ReturnType = new TsTypeReference("ReturnType"),
                             Body = $"// not implemented{Environment.NewLine}//todo do"
                         };
            tsName.ShouldBeTranslatedTo("function SomeFunction(firstParameter: FirstType, secondParameter: SecondType): ReturnType {",
                                        "    // not implemented",
                                        "    //todo do",
                                        "}");
        }
    }
}
