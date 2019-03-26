using System;
using System.Collections.Generic;

namespace LazyCoder.CSharp
{
    public class CsClass: CsDeclaration
    {
        public IEnumerable<CsTypeMember> Members { get; set; } = Array.Empty<CsTypeMember>();
        public IEnumerable<CsType> Generics { get; set; } = Array.Empty<CsType>();
    }
}
