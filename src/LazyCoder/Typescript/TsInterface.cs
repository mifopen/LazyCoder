using System;
using System.Collections.Generic;

namespace LazyCoder.Typescript
{
    public class TsInterface: TsDeclaration
    {
        public IEnumerable<TsType> Base { get; set; } = Array.Empty<TsType>();
        public string[] TypeParameters { get; set; } = Array.Empty<string>();
        public IEnumerable<TsTypeMember> Properties { get; set; } = Array.Empty<TsTypeMember>();
    }
}
