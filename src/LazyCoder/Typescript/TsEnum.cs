using System.Collections.Generic;

namespace LazyCoder.Typescript
{
    public class TsEnum : ITsDeclaration
    {
        public TsExportType ExportType { get; set; }
        public string Name { get; set; }
        public IEnumerable<TsEnumValue> Values { get; set; }
    }
}
