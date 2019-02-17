using System;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Mono.Cecil;

namespace LazyCoder.Runner
{
    class Program
    {
        static void Main(string[] args)
        {
            var dllPath =
                Path.GetFullPath("../../tests/LazyCoder.TestDll/bin/Debug/netstandard2.0/LazyCoder.TestDll.dll");
            var folder = Path.GetDirectoryName(dllPath);
            var readerParameters = new ReaderParameters
                                   {
                                       ReadingMode = ReadingMode.Deferred,
                                       AssemblyResolver = new AssemblyResolver(folder)
                                   };
            var moduleDefinition = ModuleDefinition.ReadModule(dllPath, readerParameters);
            var coders = Assembly.LoadFile(dllPath)
                                 .GetTypes()
                                 .Where(x => x.GetInterfaces()
                                              .Any(y => y == typeof(ICoder)))
                                 .ToArray();
            foreach (var coderType in coders)
            {
                Console.Out.WriteLine("coder " + coderType.Name);
                var ctor = (Func<ICoder>)Expression.Lambda(Expression.New(coderType)).Compile();
                var coder = ctor();
                var tsFiles = coder.Rewrite(moduleDefinition);
            }
        }
    }
}
