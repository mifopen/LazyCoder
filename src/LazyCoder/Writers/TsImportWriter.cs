using System.Linq;
using LazyCoder.Typescript;

namespace LazyCoder.Writers
{
    internal class TsImportWriter: ITsWriter<TsImport>
    {
        public void Write(IKeyboard keyboard,
                          TsImport tsImport)
        {
            keyboard.Type("import ");
            if (!string.IsNullOrEmpty(tsImport.Default))
                keyboard.Type(tsImport.Default);

            if (!string.IsNullOrEmpty(tsImport.Default)
                && tsImport.Named.Any())
                keyboard.Type(", ");

            if (tsImport.Named.Any())
            {
                keyboard.Type("{ ");
                var names = tsImport.Named.ToArray();
                for (int i = 0; i < names.Length; i++)
                {
                    keyboard.Type(names[i]);
                    if (i != names.Length - 1)
                        keyboard.Type(", ");
                }

                keyboard.Type(" }");
            }

            keyboard.Type(" from \"", tsImport.Path, "\";")
                    .NewLine();
        }
    }
}
