using LazyCoder.Typescript;
using Xunit;

namespace LazyCoder.Tests
{
    public class TsNameWriterTest
    {
        [Fact]
        public void Simple()
        {
            var tsName = new TsName
                         {
                             Value = "SomeName",
                             Generics = new[]
                                        {
                                            new TsName { Value = "FirstGeneric" },
                                            new TsName
                                            {
                                                Value = "SecondGeneric",
                                                Generics = new[]
                                                           {
                                                               new TsName
                                                               {
                                                                   Value = "NestedGeneric"
                                                               }
                                                           }
                                            },
                                        }
                         };
            tsName.ShouldBeTranslatedTo("SomeName<FirstGeneric, SecondGeneric<NestedGeneric>>");
        }
    }
}
