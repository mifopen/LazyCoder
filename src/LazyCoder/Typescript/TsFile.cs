using System;

namespace LazyCoder.Typescript
{
    public class TsFile
    {
        public string Name { get; set; }
        public string Directory { get; set; }
        public TsImport[] Imports { get; set; } = Array.Empty<TsImport>();
        public TsDeclaration[] Declarations { get; set; } = Array.Empty<TsDeclaration>();
    }
}
