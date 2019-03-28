using System.Collections.Generic;
using System.IO;
using System.Linq;
using LazyCoder.CSharp;
using LazyCoder.Typescript;

namespace LazyCoder.Tests
{
    public class TestCoder: ICoder
    {
        public IEnumerable<TsFile> Rewrite(IEnumerable<CsDeclaration> types)
        {
            return types
                   .OfType<CsEnum>()
                   .Select(x => new TsFile
                                {
                                    Name = x.Name,
                                    Directory =
                                        x.Namespace.Replace('.', Path.DirectorySeparatorChar),
                                    Declarations = new[]
                                                   {
                                                       new TsEnum
                                                       {
                                                           Name = x.Name,
                                                           ExportKind = TsExportKind.Named,
                                                           Values = x.Values
                                                                     .Select(y =>
                                                                                 new
                                                                                 TsEnumNumberValue
                                                                                 {
                                                                                     Name = y.Name,
                                                                                     Value = y.Value
                                                                                 })
                                                                     .ToArray()
                                                       }
                                                   }
                                });
        }
    }
}
