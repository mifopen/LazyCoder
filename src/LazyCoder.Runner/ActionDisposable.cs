using System;

namespace LazyCoder.Runner
{
    public class ActionDisposable : IDisposable
    {
        private readonly Action action;

        public ActionDisposable(Action action)
        {
            this.action = action;
        }

        public void Dispose()
        {
            this.action();
        }
    }
}