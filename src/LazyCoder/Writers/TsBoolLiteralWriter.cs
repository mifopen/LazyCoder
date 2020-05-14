using LazyCoder.Typescript;

namespace LazyCoder.Writers
{
    internal class TsBoolLiteralWriter: ITsWriter<TsBoolLiteralType>
    {
        public void Write(IKeyboard keyboard, TsBoolLiteralType tsBoolLiteralType)
        {
            keyboard.Write(tsBoolLiteralType.Bool);
        }
    }
}
