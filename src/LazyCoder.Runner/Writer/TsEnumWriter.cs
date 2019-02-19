using System;
using LazyCoder.Typescript;

namespace LazyCoder.Runner.Writer
{
    public class TsEnumWriter : ITsWriter<TsEnum>
    {
        public void Write(IKeyboard keyboard, TsEnum tsEnum)
        {
            keyboard.Write(tsEnum.ExportKind);
            keyboard.Write(tsEnum.Name);
            keyboard.Type(" ");
            using (keyboard.Block())
            {
                foreach (var tsEnumValue in tsEnum.Values)
                {
                    keyboard.IndentAndType(tsEnumValue.Name, " = ");
                    switch (tsEnumValue)
                    {
                        case TsEnumNumberValue numberValue:
                            keyboard.Write(numberValue.Value);
                            break;
                        case TsEnumStringValue stringValue:
                            keyboard.Write(stringValue.Value);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(tsEnumValue), tsEnumValue, null);
                    }

                    keyboard.TypeLine(",");
                }
            }
        }
    }
}
