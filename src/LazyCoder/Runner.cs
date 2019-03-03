using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LazyCoder.Typescript;
using LazyCoder.Writers;

namespace LazyCoder
{
    public static class Runner
    {
        public static IEnumerable<TsFile> Convert(IEnumerable<Type> types,
                                                  IEnumerable<ICoder> coders)
        {
            var csAstTypes = CsAstFactory.Create(types).ToArray();
            foreach (var coder in coders)
            {
                var tsFiles = coder.Rewrite(csAstTypes).ToArray();
                foreach (var tsFile in tsFiles)
                {
                    yield return tsFile;
                }
            }
        }

        public static void WriteFile(string outputDirectory,
                                     TsFile tsFile)
        {
            var directory = Path.Combine(Path.GetFullPath(outputDirectory), tsFile.Directory);
            Directory.CreateDirectory(directory);
            var writerContext = new WriterContext();
            writerContext.Write(tsFile);
            var content = writerContext.GetResult();
            File.WriteAllText(Path.Combine(directory, tsFile.Name + ".ts"), content);
        }
    }
}
