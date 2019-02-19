using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LazyCoder.Typescript;

namespace LazyCoder.TestDll
{
    public class TestCoder : ICoder
    {
        public IEnumerable<TsFile> Rewrite(Type[] types)
        {
            return types.Where(x => x.IsEnum)
                        .Select(x => new TsFile
                                     {
                                         Name = x.Name,
                                         Directory = x.Namespace.Replace('.', Path.DirectorySeparatorChar),
                                         Declarations = new[]
                                                        {
                                                            new TsEnum
                                                            {
                                                                Name = new TsName
                                                                       {
                                                                           Value = x.Name
                                                                       },
                                                                ExportKind = TsExportKind.Named,
                                                                Values = x.GetFields()
                                                                          .Where(f => f.Name != "value__")
                                                                          .Select(y => new TsEnumNumberValue
                                                                                       {
                                                                                           Name = y.Name,
                                                                                           Value = (int)y.GetRawConstantValue()
                                                                                       })
                                                            }
                                                        }
                                     });
        }
    }
}
