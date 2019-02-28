using System.IO;
using System.Linq;
using LazyCoder.Runner;
using LazyCoder.TestDll;
using LazyCoder.Typescript;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Engine.ClientProtocol;
using Xunit;

namespace LazyCoder.Tests
{
    public class TestCoderTest
    {
        [Fact]
        public void Simple()
        {
            var dllPath =
                Path.GetFullPath("../../../../LazyCoder.TestDll/bin/Debug/netstandard2.0/LazyCoder.TestDll.dll");
            var types = AssemblyReader.Read(dllPath);
            var csAstTypes = CsAstFactory.Create(types);
            var testCoder = new TestCoder();
            var tsFiles = testCoder.Rewrite(csAstTypes);
            foreach (var tsFile in tsFiles)
            {
                foreach (var tsDeclaration in tsFile.Declarations)
                {
                    tsDeclaration.ShouldBeTranslatedTo("export enum SomeEnum {",
                                                       "    FirstValue = 0,",
                                                       "    SecondValue = 1,",
                                                       "    ThirdValue = 2,",
                                                       "}");
                }
            }
        }
    }
}
