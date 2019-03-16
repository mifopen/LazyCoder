using System;
using LazyCoder.Typescript;

namespace LazyCoder.Writers
{
    internal class TsEnumWriter: ITsWriter<TsEnum>
    {
        public void Write(IKeyboard keyboard,
                          TsEnum tsEnum)
        {
            keyboard.Write(tsEnum.ExportKind)
                    .Type("enum ", tsEnum.Name, " ");
            using (keyboard.Block())
            {
                foreach (var tsEnumValue in tsEnum.Values)
                {
                    using (keyboard.Line())
                    {
                        keyboard.Type(tsEnumValue.Name, " = ");
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

                        keyboard.Type(",");
                    }
                }
            }
        }
    }
}
