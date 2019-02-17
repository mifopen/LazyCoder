using System.Collections.Generic;
using LazyCoder.Typescript;
using Mono.Cecil;

namespace LazyCoder
{
    public interface ICoder
    {
        IEnumerable<TsFile> Rewrite(ModuleDefinition assembly);
    }
}
