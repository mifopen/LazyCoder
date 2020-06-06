using System.Linq;
using LazyCoder.CSharp;
using LazyCoder.Walkers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Shouldly;
using Xunit;

namespace LazyCoder.Tests.WalkerTests
{
    public class ClassWalkerTest
    {
        [Fact]
        public void Properties()
        {
            var csEnum = GetCsClass(@"
namespace SomeNamespace
{
    public class SomeClass
    {
        public string FirstProperty { get; set; }
        public static string StaticProperty { get; set; }
    }
}");

            csEnum.Name.ShouldBe("SomeClass");
            csEnum.Namespace.ShouldBe("SomeNamespace");
            var firstProperty = csEnum.Members
                                      .Single(x => x.Name == "FirstProperty")
                                      .ShouldBeOfType<CsProperty>();
            firstProperty.AccessModifier.ShouldBe(CsAccessModifier.Public);
            firstProperty.IsStatic.ShouldBeFalse();

            var staticProperty = csEnum.Members
                                       .Single(x => x.Name == "StaticProperty")
                                       .ShouldBeOfType<CsProperty>();
            staticProperty.IsStatic.ShouldBeTrue();
        }

        private static CsClass GetCsClass(string text)
        {
            var tree = CSharpSyntaxTree.ParseText(text);
            var compilation = CSharpCompilation.Create("Test",
                                                       new[]
                                                       {
                                                           tree
                                                       },
                                                       new[]
                                                       {
                                                           MetadataReference.CreateFromFile(
                                                               typeof(string).Assembly.Location)
                                                       });
            var semanticModel = compilation.GetSemanticModel(tree);
            var classWalker = new ClassWalker(semanticModel);
            var classDeclarationSyntax = GetClassDeclarationSyntax(tree);
            return classWalker.Visit(classDeclarationSyntax);
        }

        private static ClassDeclarationSyntax GetClassDeclarationSyntax(SyntaxTree syntaxTree)
        {
            return syntaxTree.GetRoot()
                             .DescendantNodes()
                             .OfType<ClassDeclarationSyntax>()
                             .Single();
        }
    }
}