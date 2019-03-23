using System.Collections.Generic;

namespace LazyCoder.CSharp
{
    public class CsEnum: CsDeclaration
    {
        public IEnumerable<CsEnumValue> Values { get; set; }
    }
}
