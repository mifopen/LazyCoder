namespace LazyCoder.Runner.Writer
{
    public interface ITsWriter<in T>
    {
        void Write(IKeyboard keyboard, T tsThing);
    }
}
