using System.Collections.Generic;

namespace LazyCoder.Typescript
{
    public class TsNamespace
    {
        public string Name { get; set; }
        public TsExportKind ExportKind { get; set; }
        public IEnumerable<ITsDeclaration> Declarations { get; set; }
    }
}
