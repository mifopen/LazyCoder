using System;

namespace LazyCoder.CSharp
{
    public abstract class CsType
    {
        public string Name { get; set; }
        public string Namespace { get; set; }
        public Type OriginalType { get; set; }
    }
}
