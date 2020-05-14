using LazyCoder.Typescript;

namespace LazyCoder.Writers
{
    internal class TsStringLiteralWriter: ITsWriter<TsStringLiteralType>
    {
        public void Write(IKeyboard keyboard, TsStringLiteralType tsStringLiteralType)
        {
            keyboard.Write(tsStringLiteralType.String);
        }
    }
}
