using System;

namespace LazyCoder.CSharp
{
    public class CsClass: CsDeclaration
    {
        public CsClass(Type type): base(type)
        {
        }

        public CsTypeMember[] Members { get; set; } = Array.Empty<CsTypeMember>();
        public string[] TypeParameters { get; set; } = Array.Empty<string>();
    }
}
