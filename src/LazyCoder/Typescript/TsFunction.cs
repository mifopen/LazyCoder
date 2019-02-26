using System;
using System.Collections.Generic;

namespace LazyCoder.Typescript
{
    public class TsFunction : TsDeclaration
    {
        public TsType ReturnType { get; set; }
        public IEnumerable<TsFunctionParameter> Parameters { get; set; } = Array.Empty<TsFunctionParameter>();
        public string Body { get; set; }
    }
}
