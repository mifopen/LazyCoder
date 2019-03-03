using System;
using System.Collections.Generic;

namespace LazyCoder.Typescript
{
    public class TsEnum: TsDeclaration
    {
        public IEnumerable<TsEnumValue> Values { get; set; } = Array.Empty<TsEnumValue>();
    }
}
