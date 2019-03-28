using System.IO;
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
                                  .GroupBy(x => ( x.Path, x.RelativeToOutputDirectoryPath ))
                                  .Select(x => new TsImport
                                               {
                                                   Default =
                                                       x.SingleOrDefault(y =>
                                                                             !string
                                                                                 .IsNullOrEmpty(y.Default))
                                                        ?.Default,
                                                   Named = x.SelectMany(y => y.Named).Distinct()
                                                            .OrderBy(y => y)
                                                            .ToArray(),
                                                   Path = GetPath(x.Key.Path,
                                                                  x.Key
                                                                   .RelativeToOutputDirectoryPath,
                                                                  tsFile)
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

            for (var i = 0; i < tsFile.Declarations.Length; i++)
            {
                keyboard.Write(tsFile.Declarations[i]);
                if (i != tsFile.Declarations.Length - 1)
                    keyboard.NewLine();
            }

            keyboard.EnsureNewLine();
        }

        private static string GetPath(string path,
                                      string relativeToOutputDirectoryPath,
                                      TsFile tsFile)
        {
            if (!string.IsNullOrEmpty(path))
                return path;

            return Helpers.GetPathFromAToB(tsFile.Directory.Split(Path.DirectorySeparatorChar),
                                           relativeToOutputDirectoryPath
                                               .Split(Path.DirectorySeparatorChar));
        }
    }
}
