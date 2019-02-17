using System.Collections.Generic;
using System.IO;
using System.Linq;
using LazyCoder.Typescript;
using Mono.Cecil;

namespace LazyCoder.TestDll
{
    public class TestCoder : ICoder
    {
        public IEnumerable<TsFile> Rewrite(ModuleDefinition assembly)
        {
            return assembly.Types
                           .Where(x => x.IsEnum)
                           .Select(x => new TsFile
                                        {
                                            Name = x.Name,
                                            Path = x.Namespace.Replace('.', Path.DirectorySeparatorChar),
                                            Declarations = new[]
                                                           {
                                                               new TsEnum
                                                               {
                                                                   Name = new TsName
                                                                          {
                                                                              Value = x.Name
                                                                          },
                                                                   ExportKind = TsExportKind.Named,
                                                                   Values = x.Fields
                                                                             .Where(f => f.Name != "value__")
                                                                             .Select(y => new TsEnumNumberValue
                                                                                          {
                                                                                              Name = y.Name,
                                                                                              Value = (int)y
                                                                                                  .Constant
                                                                                          })
                                                               }
                                                           }
                                        });
        }
    }
}
