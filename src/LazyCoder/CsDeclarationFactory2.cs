using System.Linq;
using LazyCoder.CSharp;
using LazyCoder.Walkers;
using Microsoft.CodeAnalysis;

namespace LazyCoder
{
    internal static class CsDeclarationFactory2
    {
        public static CsDeclaration[] Create(Compilation compilation)
        {
            return compilation.SyntaxTrees
                              .SelectMany(tree =>
                                          {
                                              var model = compilation.GetSemanticModel(tree);
                                              var customWalker = new TheWalker(model);
                                              return customWalker.Visit(tree.GetRoot());
                                          })
                              .ToArray();
        }

        public static CsDeclaration Create(Compilation compilation, ISymbol symbol)
        {
            var reference = symbol.DeclaringSyntaxReferences.Single();
            var tree = reference.SyntaxTree;
            var model = compilation.GetSemanticModel(tree);
            var customWalker = new TheWalker(model);
            var node = reference.GetSyntax();
            return customWalker.Visit(node).Single();
        }
    }
}