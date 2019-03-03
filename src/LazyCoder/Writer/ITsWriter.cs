namespace LazyCoder.Writer
{
    public interface ITsWriter<in T>
    {
        void Write(IKeyboard keyboard, T tsThing);
    }
}
