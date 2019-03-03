namespace LazyCoder.Writers
{
    internal interface ITsWriter<in T>
    {
        void Write(IKeyboard keyboard, T tsThing);
    }
}
