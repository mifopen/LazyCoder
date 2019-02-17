using System;
using System.Linq;
using LazyCoder.Typescript;

namespace LazyCoder.Runner.Writer
{
    public class TsFunctionWriter : ITsWriter<TsFunction>
    {
        public void Write(IKeyboard keyboard, TsFunction tsFunction)
        {
            keyboard.Write(tsFunction.ExportKind);
            keyboard.Type("function ");
            keyboard.Write(tsFunction.Name);
            keyboard.Type("(");
            var parameters = tsFunction.Parameters.ToArray();
            for (var i = 0; i < parameters.Length; i++)
            {
                var parameter = parameters[i];
                keyboard.Type(parameter.Name, ": ");
                keyboard.Write(parameter.Type);
                if (i != parameters.Length - 1)
                    keyboard.Type(", ");
            }

            keyboard.Type(")");
            if (tsFunction.ReturnType != null)
            {
                keyboard.Type(": ");
                keyboard.Write(tsFunction.ReturnType);
            }

            keyboard.Type(" ");
            using (keyboard.Block())
            {
                var bodyLines = tsFunction.Body.Split(Environment.NewLine);
                foreach (var bodyLine in bodyLines)
                {
                    keyboard.TypeIndent();
                    keyboard.TypeLine(bodyLine);
                }
            }
        }
    }
}
