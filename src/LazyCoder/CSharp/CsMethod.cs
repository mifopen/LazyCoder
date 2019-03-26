using System.Collections.Generic;
using System.Reflection;

namespace LazyCoder.CSharp
{
    public class CsMethod: CsTypeMember
    {
        public CsType ReturnType { get; set; }
        public IEnumerable<CsMethodParameter> Parameters { get; set; }
        public MethodInfo OriginalMethod { get; set; }
    }
}
