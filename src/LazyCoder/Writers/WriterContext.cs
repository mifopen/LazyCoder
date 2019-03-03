using System;
using System.Text;

namespace LazyCoder.Writers
{
    internal class WriterContext: IKeyboard
    {
        private readonly StringBuilder sb = new StringBuilder();
        private int indentLevel;

        public IKeyboard Type(params string[] words)
        {
            sb.Append(string.Join("", words));
            return this;
        }

        public IKeyboard NewLine()
        {
            sb.AppendLine();
            return this;
        }

        private IDisposable IncreaseIndent()
        {
            indentLevel++;
            return new ActionDisposable(() => indentLevel--);
        }

        public IKeyboard Write<T>(T tsThing)
        {
            var writer = TsWriterFactory.CreateFor(tsThing.GetType());
            writer.Write(this, tsThing);
            return this;
        }

        public IDisposable Block()
        {
            Type("{").NewLine();
            var unindent = IncreaseIndent();
            return new ActionDisposable(() =>
                                        {
                                            unindent.Dispose();
                                            Indent().Type("}").NewLine();
                                        });
        }

        public IDisposable Line()
        {
            Indent();
            return new ActionDisposable(() => NewLine());
        }

        public IKeyboard Indent()
        {
            sb.Append(new string(' ', indentLevel * 4));
            return this;
        }

        public string GetResult()
        {
            return sb.ToString();
        }
    }
}
