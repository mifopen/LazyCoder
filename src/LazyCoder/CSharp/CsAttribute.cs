using System;
using Microsoft.CodeAnalysis;

namespace LazyCoder.CSharp
{
    public class CsAttribute
    {
        public string Name { get; set; }
        public Type OriginalType { get; set; }
        public ITypeSymbol TypeSymbol { get; set; }
    }
}