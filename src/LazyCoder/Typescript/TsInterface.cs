using System.Collections.Generic;

namespace LazyCoder.Typescript
{
    public class TsInterface: TsDeclaration
    {
        public IEnumerable<TsName> Base { get; set; }
        public IEnumerable<TsInterfaceProperty> Properties { get; set; }
    }
}
