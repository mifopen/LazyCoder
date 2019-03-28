using System;

namespace LazyCoder.CSharp
{
    public class CsEnum: CsDeclaration
    {
        public CsEnum(Type type): base(type)
        {
        }

        public CsEnumValue[] Values { get; set; } = Array.Empty<CsEnumValue>();
    }
}
