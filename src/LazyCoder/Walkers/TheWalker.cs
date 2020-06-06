using System.Collections.Generic;
using LazyCoder.CSharp;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace LazyCoder.Walkers
{
    internal class TheWalker: BaseWalker<List<CsDeclaration>>
    {
        public TheWalker(SemanticModel semanticModel)
            : base(semanticModel)
        {
            result = new List<CsDeclaration>();
        }

        public override void VisitEnumDeclaration(EnumDeclarationSyntax node)
        {
            var enumWalker = new EnumWalker(semanticModel);
            result!.Add(enumWalker.Visit(node)!);
            base.VisitEnumDeclaration(node);
        }

        public override void VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            var classWalker = new ClassWalker(semanticModel);
            result!.Add(classWalker.Visit(node)!);
            base.VisitClassDeclaration(node);
        }
    }
}