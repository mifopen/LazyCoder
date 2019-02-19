using System.Linq;
using LazyCoder.Typescript;

namespace LazyCoder.Runner.Writer
{
    public class TsNamespaceWriter : ITsWriter<TsNamespace>
    {
        public void Write(IKeyboard keyboard, TsNamespace tsNamespace)
        {
            keyboard.Write(tsNamespace.ExportKind);
            keyboard.IndentAndType("namespace ", tsNamespace.Name, " ");
            using (keyboard.Block())
            {
                var declarations = tsNamespace.Declarations.ToArray();
                for (var i = 0; i < declarations.Length; i++)
                {
                    keyboard.Write(declarations[i]);
                    if (i != declarations.Length - 1)
                        keyboard.NewLine();
                }
            }
        }
    }
}
