using LazyCoder.Typescript;
using Xunit;

namespace LazyCoder.Tests
{
    public class TsImportWriterTest
    {
        [Fact]
        public void Simple()
        {
            var tsImport = new TsImport
                           {
                               Default = "DefaultImport",
                               Named = new[] { "FirstName", "SecondName" },
                               Path = "some/path"
                           };
            tsImport.ShouldBeTranslatedTo("import DefaultImport, { FirstName, SecondName } from \"some/path\";");
        }
    }
}
