using System;
using Microsoft.CodeAnalysis;

namespace LazyCoder.CSharp
{
    public abstract class CsTypeDeclaration: CsBaseTypeDeclaration
    {
        protected CsTypeDeclaration(Type type)
            : base(type)
        {
        }

        protected CsTypeDeclaration(ITypeSymbol typeSymbol)
            : base(typeSymbol)
        {
        }

        public string[] TypeParameters { get; set; } = Array.Empty<string>();
        public CsMember[] Members { get; set; } = Array.Empty<CsMember>();
    }
}