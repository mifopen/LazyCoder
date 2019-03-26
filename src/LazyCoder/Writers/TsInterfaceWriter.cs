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
                    .Type("interface ", tsInterface.Name, " ");
            using (keyboard.Block())
            {
                foreach (var tsInterfaceProperty in tsInterface.Properties.OfType<TsPropertySignature>())
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
        }
    }
}
