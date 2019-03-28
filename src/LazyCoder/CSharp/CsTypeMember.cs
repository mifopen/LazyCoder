using System;

namespace LazyCoder.CSharp
{
    public abstract class CsTypeMember
    {
        public string Name { get; set; }
        public bool IsStatic { get; set; }
        public bool IsInherited { get; set; }
        public CsAccessModifier AccessModifier { get; set; }
        public CsAttribute[] Attributes { get; set; } = Array.Empty<CsAttribute>();
    }
}
