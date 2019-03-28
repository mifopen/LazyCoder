using System;
using System.Collections.Generic;

namespace LazyCoder.CSharp
{
    public class CsInterface: CsDeclaration
    {
        public CsInterface(Type type): base(type)
        {
        }

        public CsTypeMember[] Members { get; set; } = Array.Empty<CsTypeMember>();
        public string[] TypeParameters { get; set; } = Array.Empty<string>();
    }
}
