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
    public class EnumWalkerTest
    {
        [Fact]
        public void Simple()
        {
            var csEnum = GetCsEnum(@"
namespace SomeNamespace
{
    public enum SomeEnum
    {
        FirstValue,
        SecondValue,
        ThirdValue
    }
}");

            csEnum.Name.ShouldBe("SomeEnum");
            csEnum.Namespace.ShouldBe("SomeNamespace");
            csEnum.Values[0].Name.ShouldBe("FirstValue");
            csEnum.Values[0].Value.ShouldBe(0);
            csEnum.Values[1].Name.ShouldBe("SecondValue");
            csEnum.Values[1].Value.ShouldBe(1);
            csEnum.Values[2].Name.ShouldBe("ThirdValue");
            csEnum.Values[2].Value.ShouldBe(2);
        }

        [Fact]
        public void EnumWithOverridenValues()
        {
            var csEnum = GetCsEnum(@"
namespace SomeNamespace
{
    public enum SomeEnum
    {
        FirstValue = 1,
        SecondValue,
        ThirdValue = 5,
        FourthValue = 1 << 5
    }
}");

            csEnum.Values[0].Value.ShouldBe(1);
            csEnum.Values[1].Value.ShouldBe(2);
            csEnum.Values[2].Value.ShouldBe(5);
            csEnum.Values[3].Value.ShouldBe(32);
        }

        private static CsEnum GetCsEnum(string text)
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
            var enumWalker = new EnumWalker(semanticModel);
            var enumDeclarationSyntax = GetEnumDeclarationSyntax(tree);
            return enumWalker.Visit(enumDeclarationSyntax);
        }

        private static EnumDeclarationSyntax GetEnumDeclarationSyntax(SyntaxTree syntaxTree)
        {
            return syntaxTree.GetRoot()
                             .DescendantNodes()
                             .OfType<EnumDeclarationSyntax>()
                             .Single();
        }
    }
}