using System;

namespace LazyCoder.Typescript
{
    public class TsClass: TsDeclaration
    {
        public TsType[] Base { get; set; } = Array.Empty<TsType>();
    }
}
