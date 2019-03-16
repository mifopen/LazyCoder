using System.Linq;
using LazyCoder.Typescript;

namespace LazyCoder.Writers
{
    internal class TsFileWriter: ITsWriter<TsFile>
    {
        public void Write(IKeyboard keyboard,
                          TsFile tsFile)
        {
            var tsImports = tsFile.Imports
                                  .Concat(tsFile.Declarations.SelectMany(ImportFinder.Find))
                                  .GroupBy(x => x.Path)
                                  .Select(x => new TsImport
                                               {
                                                   Default = x.SingleOrDefault(y => !string.IsNullOrEmpty(y.Default))?.Default,
                                                   Named = x.SelectMany(y => y.Named).Distinct().OrderBy(y => y),
                                                   Path = x.Key
                                               })
                                  .ToArray();
            foreach (var tsImport in tsImports)
            {
                keyboard.Write(tsImport);
            }

            if (tsFile.Imports.Any())
            {
                keyboard.NewLine();
            }

            foreach (var tsDeclaration in tsFile.Declarations)
            {
                keyboard.Write(tsDeclaration);
            }
        }
    }
}
