using System;

namespace LazyCoder.Typescript
{
    public class TsNamespace: TsDeclaration
    {
        public TsDeclaration[] Declarations { get; set; } = Array.Empty<TsDeclaration>();
    }
}
