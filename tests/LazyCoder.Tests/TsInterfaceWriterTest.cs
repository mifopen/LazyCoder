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
                                  Name = "SomeInterface",
                                  Properties = new[]
                                               {
                                                   new TsPropertySignature { Name = "FirstProperty", Type = TsPredefinedType.Boolean() },
                                                   new TsPropertySignature { Name = "SecondProperty", Type = TsPredefinedType.Number(), Optional = true },
                                                   new TsPropertySignature { Name = "ThirdProperty", Type = new TsUnionType(TsPredefinedType.String(), new TsNull()) },
                                                   new TsPropertySignature { Name = "FourthProperty", Type = new TsArrayType { ElementType = new TsUnionType(TsPredefinedType.String(), new TsNull()) } }
                                               }
                              };

            tsInterface.ShouldBeTranslatedTo("export interface SomeInterface {",
                                             "    FirstProperty: boolean;",
                                             "    SecondProperty?: number;",
                                             "    ThirdProperty: (string | null);",
                                             "    FourthProperty: (string | null)[];",
                                             "}");
        }
    }
}
