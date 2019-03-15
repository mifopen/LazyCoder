using LazyCoder.Typescript;
using Xunit;

namespace LazyCoder.Tests
{
    public class TsInterfaceWriterTest
    {
        [Fact]
        public void Simple()
        {
            var tsInterface = new TsInterface
                              {
                                  ExportKind = TsExportKind.Named,
                                  Name = new TsName { Value = "SomeInterface" },
                                  Properties = new[]
                                               {
                                                   new TsInterfaceProperty { Name = "FirstProperty", Type = TsPredefinedType.Boolean() },
                                                   new TsInterfaceProperty { Name = "SecondProperty", Type = TsPredefinedType.Number(), Optional = true },
                                                   new TsInterfaceProperty { Name = "ThirdProperty", Type = new TsUnionType(TsPredefinedType.String(), new TsNull()) }
                                               }
                              };
            tsInterface.ShouldBeTranslatedTo("export interface SomeInterface {",
                                             "    FirstProperty: boolean;",
                                             "    SecondProperty?: number;",
                                             "    ThirdProperty: string | null;",
                                             "}");
        }
    }
}
