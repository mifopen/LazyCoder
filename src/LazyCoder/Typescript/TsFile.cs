using System;
using System.Collections.Generic;

namespace LazyCoder.Typescript
{
    public class TsFile
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public IEnumerable<TsImport> Imports { get; set; } = Array.Empty<TsImport>();
        public IEnumerable<ITsDeclaration> Declarations { get; set; } = Array.Empty<ITsDeclaration>();
    }
}
