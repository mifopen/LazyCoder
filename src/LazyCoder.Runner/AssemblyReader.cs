using System.IO;
using Mono.Cecil;

namespace LazyCoder.Runner
{
    public class AssemblyReader
    {
        public ModuleDefinition Read(string dllPath)
        {
            var folder = Path.GetDirectoryName(dllPath);
            var readerParameters = new ReaderParameters
                                   {
                                       ReadingMode = ReadingMode.Deferred,
                                       AssemblyResolver = new AssemblyResolver(folder)
                                   };
            return ModuleDefinition.ReadModule(dllPath, readerParameters);
        }
    }
}