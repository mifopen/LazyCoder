using System.Linq;
using LazyCoder.Typescript;

namespace LazyCoder.Runner.Writer
{
    public class TsFileWriter : ITsWriter<TsFile>
    {
        public void Write(IKeyboard keyboard,
                          TsFile tsFile)
        {
            foreach (var tsImport in tsFile.Imports)
            {
                keyboard.Write(tsImport);
            }

            if (tsFile.Imports.Any())
                keyboard.NewLine();

            foreach (var tsDeclaration in tsFile.Declarations)
            {
                keyboard.Write(tsDeclaration);
            }
        }
    }
}
