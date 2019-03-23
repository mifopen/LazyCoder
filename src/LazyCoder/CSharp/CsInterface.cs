using System;
using System.Collections.Generic;

namespace LazyCoder.CSharp
{
    public class CsInterface: CsDeclaration
    {
        public IEnumerable<CsClassMember> Members { get; set; } = Array.Empty<CsClassMember>();
        public IEnumerable<CsType> Generics { get; set; } = Array.Empty<CsType>();
    }

    public class CsStruct: CsDeclaration
    {
        public IEnumerable<CsClassMember> Members { get; set; } = Array.Empty<CsClassMember>();
    }
}
