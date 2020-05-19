using System;
using Microsoft.CodeAnalysis;

namespace LazyCoder.CSharp
{
    public class CsEnum: CsBaseTypeDeclaration
    {
        public CsEnum(Type type): base(type)
        {
        }

        public CsEnum(ITypeSymbol typeSymbol): base(typeSymbol)
        {
        }

        public CsEnumValue[] Values { get; set; } = Array.Empty<CsEnumValue>();
    }
}