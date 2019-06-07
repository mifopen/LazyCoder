using LazyCoder.CSharp;
using LazyCoder.Typescript;
using Xunit;

namespace LazyCoder.Tests
{
    public class InheritedBaseCoderTest
    {
        [Fact]
        public void Simple()
        {
            var inheritedBaseCoder = new InheritedBaseCoder();
            var tsFile = inheritedBaseCoder.Rewrite(CsDeclarationFactory.Create(typeof(SomeEnum)));

            foreach (var tsDeclaration in tsFile.Declarations)
            {
                tsDeclaration.ShouldBeTranslatedTo("export enum SomeEnum {",
                                                   "    FirstValue = \"FirstValue\",",
                                                   "    SecondValue = \"SecondValue\",",
                                                   "    ThirdValue = \"ThirdValue\",",
                                                   "}");
            }
        }

        private class InheritedBaseCoder: BaseCoder
        {
            protected override TsEnumValue Rewrite(CsEnumValue x)
            {
                return new TsEnumStringValue
                       {
                           Name = x.Name, Value = x.Name
                       };
            }
        }
    }
}
