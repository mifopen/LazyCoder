using System.Collections.Generic;
using LazyCoder.Typescript;

namespace LazyCoder.Writers
{
    internal class TsIdentifierWriter: ITsWriter<TsIdentifier>
    {
        public void Write(IKeyboard keyboard,
                          TsIdentifier tsIdentifier)
        {
            var value = tsIdentifier.Value;
            if (keywods.Contains(value))
                value += "_";
            keyboard.Type(value);
        }

        private static readonly HashSet<string> keywods = new HashSet<string>
                                                          {
                                                              "break",
                                                              "case",
                                                              "catch",
                                                              "class",
                                                              "const",
                                                              "continue",
                                                              "debugger",
                                                              "default",
                                                              "delete",
                                                              "do",
                                                              "else",
                                                              "enum",
                                                              "export",
                                                              "extends",
                                                              "false",
                                                              "finally",
                                                              "for",
                                                              "function",
                                                              "if",
                                                              "import",
                                                              "in",
                                                              "instanceof",
                                                              "new",
                                                              "null",
                                                              "return",
                                                              "super",
                                                              "switch",
                                                              "this",
                                                              "throw",
                                                              "true",
                                                              "try",
                                                              "typeof",
                                                              "var",
                                                              "void",
                                                              "while",
                                                              "with",
                                                              "implements",
                                                              "interface",
                                                              "let",
                                                              "package",
                                                              "private",
                                                              "protected",
                                                              "public",
                                                              "static",
                                                              "yield"
                                                          };
    }
}
