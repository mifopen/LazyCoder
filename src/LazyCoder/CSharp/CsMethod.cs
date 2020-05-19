using System;
using System.Reflection;

namespace LazyCoder.CSharp
{
    public class CsMethod: CsMember
    {
        public CsType ReturnType { get; set; }
        public CsMethodParameter[] Parameters { get; set; } = Array.Empty<CsMethodParameter>();
        public MethodInfo OriginalMethod { get; set; }
    }
}
