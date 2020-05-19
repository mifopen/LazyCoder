using System;
using Microsoft.CodeAnalysis;

namespace LazyCoder.CSharp
{
    public abstract class CsBaseTypeDeclaration: CsDeclaration
    {
        protected CsBaseTypeDeclaration(Type type): base(type)
        {
        }

        protected CsBaseTypeDeclaration(ITypeSymbol type): base(type)
        {
        }
    }
}