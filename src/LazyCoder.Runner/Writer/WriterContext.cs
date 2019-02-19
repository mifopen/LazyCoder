using System;
using System.Text;

namespace LazyCoder.Runner.Writer
{
    public class WriterContext : IKeyboard
    {
        private readonly StringBuilder sb = new StringBuilder();
        private int indentLevel;

        public void Type(params string[] words)
        {
            sb.AppendJoin("", words);
        }

        public void NewLine()
        {
            sb.AppendLine();
        }

        public void TypeLine(string line)
        {
            Type(line);
            NewLine();
        }

        public void IndentAndType(params string[] words)
        {
            TypeIndent();
            Type(words);
        }

        public void IndentAndTypeLine(string line)
        {
            TypeIndent();
            TypeLine(line);
        }

        public IDisposable Indent()
        {
            indentLevel++;
            return new ActionDisposable(() => indentLevel--);
        }

        public void Write<T>(T tsThing)
        {
            var writer = TsWriterFactory.CreateFor(tsThing.GetType());
            writer.Write(this, tsThing);
        }

        public IDisposable Block()
        {
            TypeLine("{");
            var unindent = Indent();
            return new ActionDisposable(() =>
                                        {
                                            unindent.Dispose();
                                            TypeLine("}");
                                        });
        }

        public void TypeIndent()
        {
            sb.Append(new string(' ', indentLevel * 4));
        }

        public string GetResult()
        {
            return sb.ToString().Trim();
        }
    }
}
