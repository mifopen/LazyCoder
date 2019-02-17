namespace LazyCoder.Runner.Writer
{
    public class TsNumberWriter : ITsWriter<int>
    {
        public void Write(IKeyboard keyboard, int number)
        {
            keyboard.Type(number.ToString());
        }
    }
}