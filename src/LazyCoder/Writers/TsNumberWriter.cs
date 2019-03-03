namespace LazyCoder.Writers
{
    internal class TsNumberWriter: ITsWriter<int>
    {
        public void Write(IKeyboard keyboard, int number)
        {
            keyboard.Type(number.ToString());
        }
    }
}
