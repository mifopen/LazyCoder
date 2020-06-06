using System.Collections.Generic;
using LazyCoder.CSharp;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace LazyCoder.Walkers
{
    internal class EnumWalker: BaseWalker<CsEnum>
    {
        private readonly List<CsEnumValue> values = new List<CsEnumValue>();

        public EnumWalker(SemanticModel semanticModel)
            : base(semanticModel)
        {
        }

        public override void VisitEnumDeclaration(EnumDeclarationSyntax node)
        {
            base.VisitEnumDeclaration(node);
            var symbol = semanticModel.GetDeclaredSymbol(node)!;
            result = new CsEnum(symbol)
                     {
                         Values = values.ToArray()
                     };
        }

        public override void VisitEnumMemberDeclaration(EnumMemberDeclarationSyntax node)
        {
            var symbol = semanticModel.GetDeclaredSymbol(node)!;
            values.Add(new CsEnumValue
                       {
                           Name = node.Identifier.Text,
                           Value = (int)symbol.ConstantValue!
                       });
            base.VisitEnumMemberDeclaration(node);
        }
    }
}