using System;
using System.Collections.Generic;

namespace LazyCoder.CSharp
{
    public class CsStruct: CsDeclaration
    {
        public CsStruct(Type type): base(type)
        {
        }

        public IEnumerable<CsTypeMember> Members { get; set; } = Array.Empty<CsTypeMember>();
    }
}
