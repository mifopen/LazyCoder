using LazyCoder.Typescript;

namespace LazyCoder.Writer
{
    public class TsInterfaceWriter: ITsWriter<TsInterface>
    {
        public void Write(IKeyboard keyboard,
                          TsInterface tsInterface)
        {
            keyboard.Write(tsInterface.ExportKind)
                    .Type("interface ")
                    .Write(tsInterface.Name)
                    .Type(" ");
            using (keyboard.Block())
            {
                foreach (var tsInterfaceProperty in tsInterface.Properties)
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
