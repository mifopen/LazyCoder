using System.Linq;
using Xunit;

namespace LazyCoder.Tests
{
    public class TestCoderTest
    {
        [Fact]
        public void Simple()
        {
            var tsFiles = Runner.Convert(new[]
                                     {
                                         typeof(SomeEnum),
                                         typeof(SomeController)
                                     },
                                     new ICoder[]
                                     {
                                         new TestCoder(),
                                         new TestControllerCoder()
                                     })
                                .ToArray();

            var enumTsFile = tsFiles.Single(x => x.Name == "SomeEnum");

            foreach (var tsDeclaration in enumTsFile.Declarations)
            {
                tsDeclaration.ShouldBeTranslatedTo("export enum SomeEnum {",
                                                   "    FirstValue = 0,",
                                                   "    SecondValue = 1,",
                                                   "    ThirdValue = 2,",
                                                   "}");
            }

            var controllerTsFile = tsFiles.Single(x => x.Name == "SomeApi");

            foreach (var tsDeclaration in controllerTsFile.Declarations)
            {
                tsDeclaration.ShouldBeTranslatedTo("export namespace SomeApi {",
                                                   "    export function SomeAction(value: string): number {",
                                                   "        // some body",
                                                   "    }",
                                                   "",
                                                   "    export function SomeOtherAction(value: number): boolean {",
                                                   "        // some body",
                                                   "    }",
                                                   "}");
            }
        }
    }
}
