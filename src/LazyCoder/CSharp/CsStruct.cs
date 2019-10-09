using System;

namespace LazyCoder.CSharp
{
    public class CsStruct: CsDeclaration
    {
        public CsStruct(Type type): base(type)
        {
        }

        public CsTypeMember[] Members { get; set; } = Array.Empty<CsTypeMember>();
        public string[] TypeParameters { get; set; } = Array.Empty<string>();
    }
}
