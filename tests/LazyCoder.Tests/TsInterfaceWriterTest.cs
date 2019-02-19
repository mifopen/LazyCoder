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
                                                   new TsInterfaceProperty
                                                   {
                                                       Name = "FirstProperty",
                                                       Type = new TsType { Name = "boolean" }
                                                   },
                                                   new TsInterfaceProperty
                                                   {
                                                       Name = "SecondProperty",
                                                       Type = new TsType { Name = "number" },
                                                       Optional = true
                                                   },
                                                   new TsInterfaceProperty
                                                   {
                                                       Name = "ThirdProperty",
                                                       Type = new TsType { Name = "string", Nullable = true }
                                                   }
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
