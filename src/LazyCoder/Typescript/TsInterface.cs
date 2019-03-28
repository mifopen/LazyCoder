using System;

namespace LazyCoder.Typescript
{
    public class TsInterface: TsDeclaration
    {
        public TsType[] Base { get; set; } = Array.Empty<TsType>();
        public string[] TypeParameters { get; set; } = Array.Empty<string>();
        public TsTypeMember[] Properties { get; set; } = Array.Empty<TsTypeMember>();
    }
}
