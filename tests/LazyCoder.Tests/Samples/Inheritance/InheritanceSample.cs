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
                                                typeof(Contract), typeof(BaseClass),
                                                typeof(ChildClass), typeof(GrandChildClass)
                                            },
                                            new[]
                                            {
                                                new ContractCoder()
                                            });

            tsFiles.Select(x => x.Name)
                   .ShouldBe(new[]
                             {
                                 "Contract", "BaseClass", "ChildClass", "GrandChildClass"
                             },
                             ignoreOrder: true);

            Converter.WriteFileToString(tsFiles.Single(x => x.Name == "Contract"))
                     .ShouldBeLines("import { BaseClass } from \"./BaseClass\";",
                                    "",
                                    "export interface Contract {",
                                    "    BaseClass: BaseClass;",
                                    "}",
                                    "");

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

            Converter.WriteFileToString(tsFiles.Single(x => x.Name == "GrandChildClass"))
                     .ShouldBeLines("import { ChildClass } from \"./ChildClass\";",
                                    "",
                                    "export interface GrandChildClass extends ChildClass {",
                                    "    GrandChildProperty: string;",
                                    "}",
                                    "");
        }

        private class ContractCoder: ICoder
        {
            public IEnumerable<TsFile> Rewrite(IEnumerable<CsDeclaration> csDeclarations)
            {
                return csDeclarations.Where(x => x.CsType.OriginalType == typeof(Contract))
                                     .Select(x => new DefaultCoder().Rewrite(x));
            }
        }

        private class Contract
        {
            public BaseClass BaseClass { get; set; }
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
