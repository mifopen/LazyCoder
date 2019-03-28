using System.Linq;
using LazyCoder.Typescript;

namespace LazyCoder.Writers
{
    internal class TsInterfaceWriter: ITsWriter<TsInterface>
    {
        public void Write(IKeyboard keyboard,
                          TsInterface tsInterface)
        {
            keyboard.Write(tsInterface.ExportKind)
                    .Type("interface ", tsInterface.Name);
            if (tsInterface.TypeParameters.Any())
            {
                keyboard.Type("<");
                for (var i = 0; i < tsInterface.TypeParameters.Length; i++)
                {
                    keyboard.Type(tsInterface.TypeParameters[i]);
                    if (i != tsInterface.TypeParameters.Length - 1)
                        keyboard.Type(", ");
                }

                keyboard.Type(">");
            }

            keyboard.Type(" ");
            using (keyboard.Block())
            {
                foreach (var tsInterfaceProperty in tsInterface
                                                    .Properties.OfType<TsPropertySignature>())
                {
                    using (keyboard.Line())
                    {
                        keyboard.Type(tsInterfaceProperty.Name);
                        if (tsInterfaceProperty.Optional)
                            keyboard.Type("?");
                        keyboard.Type(": ")
                                .Write(tsInterfaceProperty.Type)
                                .Type(";");
                    }
                }
            }

            keyboard.NewLine();
        }
    }
}
