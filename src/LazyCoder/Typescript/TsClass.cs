using System;
using System.Collections.Generic;

namespace LazyCoder.Typescript
{
    public class TsClass: TsDeclaration
    {
        public IEnumerable<TsType> Base { get; set; } = Array.Empty<TsType>();
    }
}
