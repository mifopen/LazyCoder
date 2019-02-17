using System;

namespace LazyCoder.Runner.Writer
{
    public interface IKeyboard
    {
        void Type(params string[] words);
        void TypeLine(string line);
        void NewLine();
        IDisposable Indent();
        void Write<T>(T tsThing);
        IDisposable Block();
        void TypeIndent();
    }
}
