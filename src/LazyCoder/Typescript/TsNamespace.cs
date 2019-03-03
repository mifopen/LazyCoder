using System.Collections.Generic;

namespace LazyCoder.Typescript
{
    public class TsNamespace: TsDeclaration
    {
        public IEnumerable<TsDeclaration> Declarations { get; set; }
    }
}
