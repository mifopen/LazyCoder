using Mono.Cecil;

namespace LazyCoder.Runner
{
    public class AssemblyResolver : DefaultAssemblyResolver
    {
        public AssemblyResolver(string folder)
        {
            this.AddSearchDirectory(folder);
        }
    }
}
