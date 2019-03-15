using System;
using System.Collections.Generic;

namespace LazyCoder.Typescript
{
    public class TsInterface: TsDeclaration
    {
        public IEnumerable<TsType> Base { get; set; } = Array.Empty<TsType>();
        public IEnumerable<TsInterfaceProperty> Properties { get; set; } = Array.Empty<TsInterfaceProperty>();
    }
}
