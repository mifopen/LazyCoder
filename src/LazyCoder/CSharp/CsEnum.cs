using System.Collections.Generic;

namespace LazyCoder.CSharp
{
    public class CsEnum: CsType
    {
        public IEnumerable<CsEnumValue> Values { get; set; }
    }
}
