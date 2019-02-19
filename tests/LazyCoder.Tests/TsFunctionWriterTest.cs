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
                             Name = new TsName { Value = "SomeFunction" },
                             Parameters = new[]
                                          {
                                              new TsFunctionParameter
                                              {
                                                  Name = "firstParameter",
                                                  Type = new TsType { Name = "FirstType" }
                                              },
                                              new TsFunctionParameter
                                              {
                                                  Name = "secondParameter",
                                                  Type = new TsType { Name = "SecondType" }
                                              }
                                          },
                             ReturnType = new TsType { Name = "ReturnType" },
                             Body = $"// not implemented{Environment.NewLine}//todo do"
                         };
            tsName.ShouldBeTranslatedTo("function SomeFunction(firstParameter: FirstType, secondParameter: SecondType): ReturnType {",
                                        "    // not implemented",
                                        "    //todo do",
                                        "}");
        }
    }
}
