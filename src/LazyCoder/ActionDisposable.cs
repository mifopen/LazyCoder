using System;

namespace LazyCoder
{
    internal class ActionDisposable: IDisposable
    {
        private readonly Action action;

        public ActionDisposable(Action action)
        {
            this.action = action;
        }

        public void Dispose()
        {
            action();
        }
    }
}
