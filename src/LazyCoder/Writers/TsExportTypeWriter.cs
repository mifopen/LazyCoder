using System;
using LazyCoder.Typescript;

namespace LazyCoder.Writers
{
    internal class TsExportTypeWriter: ITsWriter<TsExportKind>
    {
        public void Write(IKeyboard keyboard, TsExportKind tsExportKind)
        {
            keyboard.Indent();
            switch (tsExportKind)
            {
                case TsExportKind.None:
                    return;
                case TsExportKind.Named:
                    keyboard.Type("export ");
                    return;
                case TsExportKind.Default:
                    keyboard.Type("export default ");
                    return;
                default:
                    throw new ArgumentOutOfRangeException(nameof(tsExportKind), tsExportKind, null);
            }
        }
    }
}
