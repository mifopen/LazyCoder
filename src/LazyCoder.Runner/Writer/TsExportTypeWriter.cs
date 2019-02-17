using System;
using LazyCoder.Typescript;

namespace LazyCoder.Runner.Writer
{
    public class TsExportTypeWriter : ITsWriter<TsExportType>
    {
        public void Write(IKeyboard keyboard, TsExportType tsExportType)
        {
            switch (tsExportType)
            {
                case TsExportType.None:
                    return;
                case TsExportType.Export:
                    keyboard.Type("export ");
                    return;
                case TsExportType.ExportDefault:
                    keyboard.Type("export default ");
                    return;
                default:
                    throw new ArgumentOutOfRangeException(nameof(tsExportType), tsExportType, null);
            }
        }
    }
}