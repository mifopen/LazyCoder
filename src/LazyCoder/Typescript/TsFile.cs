using System.Collections.Generic;

namespace LazyCoder.Typescript
{
    public class TsFile
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public IEnumerable<TsImport> Imports { get; set; }
        public IEnumerable<TsNamespace> Namespaces { get; set; }
        public IEnumerable<ITsDeclaration> Declarations { get; set; }
    }
}
