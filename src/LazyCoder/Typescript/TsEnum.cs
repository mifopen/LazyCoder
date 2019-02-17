using System;
using System.Collections.Generic;

namespace LazyCoder.Typescript
{
    public class TsEnum : ITsDeclaration
    {
        public TsExportKind ExportKind { get; set; }
        public TsName Name { get; set; }
        public IEnumerable<TsEnumValue> Values { get; set; } = Array.Empty<TsEnumValue>();
    }
}
