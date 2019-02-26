using System.Collections.Generic;

namespace LazyCoder.CSharp
{
    public class CsMethod: CsClassMember
    {
        public CsType ReturnType { get; set; }
        public IEnumerable<CsMethodParameter> Parameters { get; set; }
    }
}
