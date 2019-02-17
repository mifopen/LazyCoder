using System.Collections.Generic;
using System.Linq;
using LazyCoder.Typescript;
using Mono.Cecil;

namespace LazyCoder.TestDll
{
    public class TestCoder : ICoder
    {
        public IEnumerable<TsFile> Rewrite(ModuleDefinition assembly)
        {
            return assembly.Types.Select(x => new TsFile
                                              {
                                                  Name = x.Name,
                                                  Folder = assembly.Types.First().Name
                                              });
        }
    }
}
