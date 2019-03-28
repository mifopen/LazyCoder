using System;

namespace LazyCoder.Typescript
{
    public class TsFunction: TsDeclaration
    {
        public TsType ReturnType { get; set; }
        public TsFunctionParameter[] Parameters { get; set; } = Array.Empty<TsFunctionParameter>();
        public string Body { get; set; }
    }
}
