using System;
using System.Collections.Generic;

namespace LazyCoder.Typescript
{
    public class TsClass : TsDeclaration
    {
        public IEnumerable<TsName> Base { get; set; } = Array.Empty<TsName>();
    }
}
