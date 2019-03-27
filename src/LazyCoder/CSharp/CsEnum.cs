using System;
using System.Collections.Generic;

namespace LazyCoder.CSharp
{
    public class CsEnum: CsDeclaration
    {
        public CsEnum(Type type): base(type)
        {
        }

        public IEnumerable<CsEnumValue> Values { get; set; }
    }
}
