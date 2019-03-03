namespace LazyCoder.Writers
{
    internal class TsStringWriter: ITsWriter<string>
    {
        public void Write(IKeyboard keyboard, string str)
        {
            keyboard.Type("\"", str, "\"");
        }
    }
}
