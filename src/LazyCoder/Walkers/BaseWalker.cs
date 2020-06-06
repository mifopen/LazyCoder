using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace LazyCoder.Walkers
{
    internal abstract class BaseWalker<T>: CSharpSyntaxWalker where T : class
    {
        protected readonly SemanticModel semanticModel;
        protected T? result;

        protected BaseWalker(SemanticModel semanticModel)
        {
            this.semanticModel = semanticModel;
        }

        public new T? Visit(SyntaxNode node)
        {
            base.Visit(node);
            return result;
        }
    }
}