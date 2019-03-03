using LazyCoder.Typescript;
using Xunit;

namespace LazyCoder.Tests
{
    public class TsFileWriterTest
    {
        [Fact]
        public void Simple()
        {
            var tsFile = new TsFile
                         {
                             Imports = new[]
                                       {
                                           new TsImport
                                           {
                                               Default = "FirstDefault",
                                               Path = "some/first/default"
                                           },
                                           new TsImport
                                           {
                                               Named = new[] { "FirstNamed" },
                                               Path = "some/first/named"
                                           }
                                       },
                             Declarations = new[]
                                            {
                                                new TsNamespace
                                                {
                                                    ExportKind = TsExportKind.Named,
                                                    Name = new TsName { Value = "SomeNamespace" },
                                                    Declarations = new[]
                                                                   {
                                                                       new TsEnum
                                                                       {
                                                                           ExportKind = TsExportKind.Named,
                                                                           Name = new TsName { Value = "SomeEnum" },
                                                                           Values = new[]
                                                                                    {
                                                                                        new TsEnumNumberValue
                                                                                        {
                                                                                            Name = "FirstEnumValue",
                                                                                            Value = 0
                                                                                        }
                                                                                    }
                                                                       }
                                                                   }
                                                }
                                            }
                         };
            tsFile.ShouldBeTranslatedTo("import FirstDefault from \"some/first/default\";",
                                        "import { FirstNamed } from \"some/first/named\";",
                                        "",
                                        "export namespace SomeNamespace {",
                                        "    export enum SomeEnum {",
                                        "        FirstEnumValue = 0,",
                                        "    }",
                                        "}");
        }
    }
}
