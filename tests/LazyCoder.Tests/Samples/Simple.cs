using System;
using System.IO;
using System.Linq;
using LazyCoder.Typescript;
using LazyCoder.Walkers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Shouldly;
using Xunit;

namespace LazyCoder.Tests.Samples
{
    public class Simple
    {
        [Fact]
        public void Test()
        {
            var tsFiles = Convert(@"
namespace SomeNamespace
{
    public enum SomeEnum
    {
        FirstValue,
        SecondValue,
        ThirdValue
    }
}");
            var tsFile = tsFiles.Single();
            var result = Converter.WriteFileToString(tsFile);
            result.ShouldBe(@"export enum SomeEnum {
    FirstValue = 0,
    SecondValue = 1,
    ThirdValue = 2,
}
");
        }

        private static TsFile[] Convert(string text)
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
            return Converter2.Convert(compilation,
                                      new[]
                                      {
                                          new DefaultCoder()
                                      });
        }
    }
}