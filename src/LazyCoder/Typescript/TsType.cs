using System;
using System.Collections.Generic;

namespace LazyCoder.Typescript
{
    public class TsType
    {
        public string Name { get; set; }
        public IEnumerable<TsType> Generics { get; set; } = Array.Empty<TsType>();
        public bool Nullable { get; set; }
    }
}
