using System;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using LazyCoder.Runner.Writer;
using LazyCoder.Typescript;

namespace LazyCoder.Runner
{
    public class Runner
    {
        public static void Run(string dll,
                               string outputDirectory
        )
        {
            dll = Path.GetFullPath(dll);
            var loadedTypes = AssemblyReader.Read(dll);
            var csAstTypes = CsAstFactory.Create(loadedTypes);
            var coderTypes = GetCoderTypes(loadedTypes);
            Console.Out.WriteLine("Found: " + string.Join(", ", coderTypes.Select(x => x.Name)));
            foreach (var coderType in coderTypes)
            {
                Console.Out.WriteLine("Start Coder " + coderType.Name);
                var ctor = (Func<ICoder>)Expression.Lambda(Expression.New(coderType)).Compile();
                var coder = ctor();
                var tsFiles = coder.Rewrite(csAstTypes).ToArray();
                Console.Out.WriteLine($"Gonna create {tsFiles.Length} files");

                Directory.CreateDirectory(Path.GetFullPath(outputDirectory));
                foreach (var tsFile in tsFiles) WriteFile(outputDirectory, tsFile);

                Console.Out.WriteLine($"Coder {coderType.Name} finished");
            }
        }

        private static void WriteFile(string outputDirectory,
                                      TsFile tsFile)
        {
            var directory = Path.Combine(Path.GetFullPath(outputDirectory), tsFile.Directory);
            Directory.CreateDirectory(directory);
            var writerContext = new WriterContext();
            writerContext.Write(tsFile);
            var content = writerContext.GetResult();
            File.WriteAllText(Path.Combine(directory, tsFile.Name + ".ts"), content);
        }

        private static Type[] GetCoderTypes(Type[] types)
        {
            return types.Where(x => x.GetInterfaces()
                                     .Any(y => y == typeof(ICoder)))
                        .ToArray();
        }
    }
}
