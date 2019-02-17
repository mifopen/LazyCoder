namespace LazyCoder.Runner.Writer
{
    public class TsStringWriter : ITsWriter<string>
    {
        public void Write(IKeyboard keyboard, string str)
        {
            keyboard.Type("\"", str, "\"");
        }
    }
}
