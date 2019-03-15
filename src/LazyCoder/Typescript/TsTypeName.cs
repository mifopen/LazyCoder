using System;

namespace LazyCoder.Typescript
{
    public class TsTypeName
    {
        public TsTypeName(string identifier)
        {
            Identifier = identifier;
        }

        public TsTypeName(string[] _namespace,
                          string identifier)
            : this(identifier)
        {
            Namespace = _namespace;
        }

        public string[] Namespace { get; } = Array.Empty<string>();
        public string Identifier { get; }
    }
}
