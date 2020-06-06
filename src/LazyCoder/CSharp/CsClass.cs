using System;
using Microsoft.CodeAnalysis;

namespace LazyCoder.CSharp
{
    public class CsClass: CsTypeDeclaration
    {
        public CsClass(Type type): base(type)
        {
        }

        public CsClass(ITypeSymbol typeSymbol)
            : base(typeSymbol)
        {
        }
    }
}