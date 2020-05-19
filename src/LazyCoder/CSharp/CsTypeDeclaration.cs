using System;

namespace LazyCoder.CSharp
{
    public abstract class CsTypeDeclaration: CsBaseTypeDeclaration
    {
        protected CsTypeDeclaration(Type type)
            : base(type)
        {
        }

        public string[] TypeParameters { get; set; } = Array.Empty<string>();
        public CsMember[] Members { get; set; } = Array.Empty<CsMember>();
    }
}