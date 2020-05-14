namespace LazyCoder.Writers
{
    internal class TsBoolWriter: ITsWriter<bool>
    {
        public void Write(IKeyboard keyboard, bool value)
        {
            keyboard.Type(value.ToString().ToLowerInvariant());
        }
    }
}
