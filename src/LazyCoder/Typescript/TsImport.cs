using System;
using System.Collections.Generic;

namespace LazyCoder.Typescript
{
    public class TsImport
    {
        public string Default { get; set; }
        public IEnumerable<string> Named { get; set; } = Array.Empty<string>();
        public string Path { get; set; }
        public string RelativeToOutputDirectoryPath { get; set; }
    }
}
