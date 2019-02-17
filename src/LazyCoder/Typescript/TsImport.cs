using System.Collections.Generic;

namespace LazyCoder.Typescript
{
    public class TsImport
    {
        public string Default { get; set; }
        public IEnumerable<string> Named { get; set; }
        public string Path { get; set; }
    }
}
