using LazyCoder.Typescript;
using Xunit;

namespace LazyCoder.Tests
{
    public class TsEnumWriterTest
    {
        [Fact]
        public void Simple()
        {
            var tsEnum = new TsEnum
                         {
                             ExportKind = TsExportKind.Default,
                             Name = "SomeEnum",
                             Values = new[]
                                      {
                                          new TsEnumNumberValue
                                          {
                                              Name = "FirstValue",
                                              Value = 0
                                          },
                                          new TsEnumNumberValue
                                          {
                                              Name = "SecondValue",
                                              Value = 1
                                          }
                                      }
                         };
            tsEnum.ShouldBeTranslatedTo("export default enum SomeEnum {",
                                        "    FirstValue = 0,",
                                        "    SecondValue = 1,",
                                        "}");
        }
    }
}
