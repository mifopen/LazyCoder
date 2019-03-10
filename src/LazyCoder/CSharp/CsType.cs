using System;
using System.Collections.Generic;

namespace LazyCoder.CSharp
{
    public abstract class CsType
    {
        public string Name { get; set; }
        public string Namespace { get; set; }
        public IEnumerable<CsAttribute> Attributes { get; set; } = Array.Empty<CsAttribute>();
        public Type OriginalType { get; set; }
    }
}
