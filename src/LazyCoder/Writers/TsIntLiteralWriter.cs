using LazyCoder.Typescript;

namespace LazyCoder.Writers
{
    internal class TsIntLiteralWriter: ITsWriter<TsIntLiteralType>
    {
        public void Write(IKeyboard keyboard, TsIntLiteralType tsIntLiteralType)
        {
            keyboard.Write(tsIntLiteralType.Int);
        }
    }
}
