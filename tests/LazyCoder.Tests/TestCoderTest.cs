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
            var moduleDefinition = new AssemblyReader().Read(dllPath);
            var testCoder = new TestCoder();
            var tsFiles = testCoder.Rewrite(moduleDefinition);
            foreach (var tsFile in tsFiles)
            {
                foreach (var tsDeclaration in tsFile.Declarations)
                {
                    tsDeclaration.ShouldBeTranslatedTo("export SomeEnum {",
                                                       "    FirstValue = 0,",
                                                       "    SecondValue = 1,",
                                                       "    ThirdValue = 2,",
                                                       "}");
                }
            }
        }
    }
}
