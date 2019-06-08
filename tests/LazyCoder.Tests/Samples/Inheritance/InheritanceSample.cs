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

            Converter.WriteFileToString(tsFiles.Single(x => x.Name == "BaseClass"))
                     .ShouldBeLines("export interface BaseClass {",
                                    "    BaseProperty: string;",
                                    "    AbstractProperty: string;",
                                    "}",
                                    "");

            Converter.WriteFileToString(tsFiles.Single(x => x.Name == "ChildClass"))
                     .ShouldBeLines("import { BaseClass } from \"./BaseClass\";",
                                    "",
                                    "export interface ChildClass extends BaseClass {",
                                    "    ChildProperty: string;",
                                    "}",
                                    "");
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
            public string BaseProperty { get; set; }
            public abstract string AbstractProperty { get; set; }
        }

        private class ChildClass: BaseClass
        {
            public string ChildProperty { get; set; }
            public override string AbstractProperty { get; set; }
        }

        private class GrandChildClass: ChildClass
        {
            public string GrandChildProperty { get; set; }
        }
    }
}
