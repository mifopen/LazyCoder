using System.Collections.Generic;
using System.Linq;
using LazyCoder.CSharp;
using LazyCoder.Typescript;
using Shouldly;
using Xunit;

namespace LazyCoder.Tests.Samples.Inheritance
{
    public class InheritanceSample
    {
        [Fact]
        public void Test()
        {
            var tsFiles = Converter.Convert(new[]
                                            {
                                                typeof(BaseClass), typeof(ChildClass),
                                                typeof(GrandChildClass)
                                            },
                                            new[]
                                            {
                                                new AbstractOnlyCoder()
                                            });

            tsFiles.Select(x => x.Name)
                   .ShouldBe(new[]
                             {
                                 "BaseClass", "ChildClass", "GrandChildClass"
                             },
                             ignoreOrder: true);
        }

        private class AbstractOnlyCoder: ICoder
        {
            public IEnumerable<TsFile> Rewrite(IEnumerable<CsDeclaration> csDeclarations)
            {
                return csDeclarations.Where(x => x.CsType.OriginalType.IsAbstract)
                                     .Select(x => new DefaultCoder().Rewrite(x));
            }
        }

        private abstract class BaseClass
        {
        }

        private class ChildClass: BaseClass
        {
        }

        private class GrandChildClass: ChildClass
        {
        }
    }
}
