using System;
using System.Collections.Generic;

namespace LazyCoder.Typescript
{
    public class TsFunction : ITsDeclaration
    {
        public TsExportKind ExportKind { get; set; }
        public TsName Name { get; set; }
        public TsName ReturnType { get; set; }
        public IEnumerable<TsFunctionParameter> Parameters { get; set; } = Array.Empty<TsFunctionParameter>();
        public string Body { get; set; }
    }
}
