using System;

namespace LazyCoder.Writers
{
    public interface IKeyboard
    {
        IKeyboard Type(params string[] words);

        IKeyboard TypeJoin(string separator,
                           params string[] words);

        IKeyboard Write<T>(T tsThing);
        IDisposable Line();
        IDisposable Block();
        IKeyboard NewLine();
        IKeyboard Indent();
    }
}
