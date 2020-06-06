using System;
using System.Collections.Generic;
using System.Linq;
using LazyCoder.CSharp;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace LazyCoder.Walkers
{
    internal class ClassWalker: BaseWalker<CsClass>
    {
        private readonly List<CsMember> members = new List<CsMember>();

        public ClassWalker(SemanticModel semanticModel): base(semanticModel)
        {
        }

        public override void VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            base.VisitClassDeclaration(node);
            var symbol = semanticModel.GetDeclaredSymbol(node)!;
            result = new CsClass(symbol)
                     {
                         Members = members.ToArray()
                     };
        }

        public override void VisitPropertyDeclaration(PropertyDeclarationSyntax node)
        {
            var symbol = semanticModel.GetDeclaredSymbol(node)!;
            members.Add(new CsProperty
                        {
                            Name = symbol.Name,
                            Type = new CsType(symbol.Type),
                            IsStatic = symbol.IsStatic,
                            AccessModifier =
                                AccessibilityToCsAccessModifier(symbol.DeclaredAccessibility),
                            Attributes = symbol.GetAttributes()
                                               .Select(x => new CsAttribute
                                                            {
                                                                Name = x.AttributeClass.Name,
                                                                TypeSymbol = x.AttributeClass
                                                            })
                                               .ToArray()
                        });
            base.VisitPropertyDeclaration(node);
        }

        private static CsAccessModifier AccessibilityToCsAccessModifier(Accessibility accessibility)
        {
            return accessibility switch
            {
                Accessibility.NotApplicable => throw new Exception(),
                Accessibility.Private => CsAccessModifier.Private,
                Accessibility.ProtectedAndInternal => CsAccessModifier.ProtectedAndInternal,
                Accessibility.Protected => CsAccessModifier.Protected,
                Accessibility.Internal => CsAccessModifier.Internal,
                Accessibility.ProtectedOrInternal => CsAccessModifier.ProtectedOrInternal,
                Accessibility.Public => CsAccessModifier.Public,
                _ => throw new ArgumentOutOfRangeException(nameof(accessibility), accessibility,
                                                           null)
            };
        }
    }
}