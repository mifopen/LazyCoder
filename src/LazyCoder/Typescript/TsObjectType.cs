using System;

namespace LazyCoder.Typescript
{
    public class TsObjectType: TsType
    {
        public TsTypeMember[] Members { get; set; } = Array.Empty<TsTypeMember>();
    }
}
