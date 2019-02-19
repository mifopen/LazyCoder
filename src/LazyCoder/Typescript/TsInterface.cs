using System.Collections.Generic;

namespace LazyCoder.Typescript
{
    public class TsInterface : ITsDeclaration
    {
        public TsName Name { get; set; }
        public IEnumerable<TsName> Base { get; set; }
        public TsExportKind ExportKind { get; set; }
        public IEnumerable<TsInterfaceProperty> Properties { get; set; }
    }
}
