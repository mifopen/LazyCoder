using System;

namespace LazyCoder.Typescript
{
    public class TsEnum: TsDeclaration
    {
        public TsEnumValue[] Values { get; set; } = Array.Empty<TsEnumValue>();
    }
}
