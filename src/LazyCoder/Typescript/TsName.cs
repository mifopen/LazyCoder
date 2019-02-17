using System;
using System.Collections.Generic;

namespace LazyCoder.Typescript
{
    public class TsName
    {
        public string Value { get; set; }
        public IEnumerable<TsName> Generics { get; set; } = Array.Empty<TsName>();
    }
}
