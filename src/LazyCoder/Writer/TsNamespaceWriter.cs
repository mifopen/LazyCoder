using System.Linq;
using LazyCoder.Typescript;

namespace LazyCoder.Writer
{
    public class TsNamespaceWriter: ITsWriter<TsNamespace>
    {
        public void Write(IKeyboard keyboard,
                          TsNamespace tsNamespace)
        {
            keyboard.Write(tsNamespace.ExportKind)
                    .Type("namespace ", tsNamespace.Name.Value, " ");
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
