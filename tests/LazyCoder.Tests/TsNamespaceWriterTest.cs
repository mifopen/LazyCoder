using LazyCoder.Typescript;
using Xunit;

namespace LazyCoder.Tests
{
    public class TsNamespaceWriterTest
    {
        [Fact]
        public void Simple()
        {
            var tsNamespace = new TsNamespace
                              {
                                  Name = "SomeNamespace",
                                  ExportKind = TsExportKind.Named,
                                  Declarations = new[]
                                                 {
                                                     new TsEnum
                                                     {
                                                         Name = "SomeEnum",
                                                         ExportKind = TsExportKind.Named,
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
                                                     }
                                                 }
                              };
            tsNamespace.ShouldBeTranslatedTo("export namespace SomeNamespace {",
                                             "    export enum SomeEnum {",
                                             "        FirstValue = 0,",
                                             "        SecondValue = 1,",
                                             "    }",
                                             "}");
        }
    }
}
