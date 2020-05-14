using LazyCoder.Typescript;

namespace LazyCoder.Writers
{
    internal class TsEnumLiteralWriter: ITsWriter<TsEnumLiteralType>
    {
        public void Write(IKeyboard keyboard, TsEnumLiteralType tsEnumLiteralType)
        {
            keyboard.Type(tsEnumLiteralType.EnumType.TypeName.Identifier)
                    .Type(".")
                    .Type(tsEnumLiteralType.Value);
        }
    }
}
