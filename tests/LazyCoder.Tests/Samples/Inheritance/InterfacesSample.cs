using System.Collections.Generic;
using System.Linq;
using LazyCoder.CSharp;
using LazyCoder.Typescript;
using Shouldly;
using Xunit;

namespace LazyCoder.Tests.Samples.Inheritance
{
    public class InterfacesSample
    {
        [Fact]
        public void Test()
        {
            var tsFiles = Converter.Convert(
                new[]
                {
                    typeof(IInterface), typeof(FirstClass), typeof(SecondClass),
                    typeof(ISecondInterface), typeof(IThirdInterface),
                },
                new[] { new ContractCoder() });

            tsFiles.Select(x => x.Name)
                .ShouldBe(new[] { "IInterface", "FirstClass", "SecondClass", "ISecondInterface", "IThirdInterface" },
                    ignoreOrder: true);

            Converter.WriteFileToString(tsFiles.Single(x => x.Name == "IInterface"))
                .ShouldBeLines("export interface IInterface {",
                    "}",
                    "");

            Converter.WriteFileToString(tsFiles.Single(x => x.Name == "FirstClass"))
                .ShouldBeLines("import { IInterface } from \"./IInterface\";",
                    "",
                    "export interface FirstClass extends IInterface {",
                    "}",
                    "");

            Converter.WriteFileToString(tsFiles.Single(x => x.Name == "ISecondInterface"))
                .ShouldBeLines("import { IInterface } from \"./IInterface\";",
                    "",
                    "export interface ISecondInterface extends IInterface {",
                    "}",
                    "");

            Converter.WriteFileToString(tsFiles.Single(x => x.Name == "SecondClass"))
                .ShouldBeLines("import { FirstClass } from \"./FirstClass\";",
                    "import { IThirdInterface } from \"./IThirdInterface\";",
                    "",
                    "export interface SecondClass extends FirstClass, IThirdInterface {",
                    "}",
                    "");
        }

        private class ContractCoder: ICoder
        {
            public IEnumerable<TsFile> Rewrite(IEnumerable<CsDeclaration> csDeclarations)
            {
                return csDeclarations.Where(x =>
                        x.CsType.OriginalType == typeof(FirstClass) ||
                        x.CsType.OriginalType == typeof(SecondClass))
                    .Select(x => new DefaultCoder().Rewrite(x));
            }
        }

        private interface IInterface
        {
        }

        private class FirstClass: IInterface
        {
        }

        private interface ISecondInterface: IInterface
        {
        }

        private interface IThirdInterface
        {
        }

        private class SecondClass: FirstClass, IThirdInterface
        {
        }
    }
}
