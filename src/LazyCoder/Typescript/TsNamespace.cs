using System.Collections.Generic;

namespace LazyCoder.Typescript
{
    public class TsNamespace
    {
        public string Name { get; set; }
        public IEnumerable<ITsDeclaration> Declarations { get; set; }
    }
}
