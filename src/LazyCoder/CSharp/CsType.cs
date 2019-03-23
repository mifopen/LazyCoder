using System;

namespace LazyCoder.CSharp
{
    public class CsType
    {
        public CsType(Type originalType)
        {
            Name = originalType.Name;
            Namespace = originalType.Namespace;
            OriginalType = originalType;
        }

        public string Name { get; }
        public string Namespace { get; }
        public Type OriginalType { get; }

        public override string ToString()
        {
            return Name;
        }
    }
}
