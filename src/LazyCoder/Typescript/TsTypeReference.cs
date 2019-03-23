using System;
using System.Collections.Generic;
using LazyCoder.CSharp;

namespace LazyCoder.Typescript
{
    public class TsTypeReference: TsType
    {
        public TsTypeReference(TsTypeName typeName)
        {
            TypeName = typeName;
        }

        public TsTypeReference(string identifier)
            : this(new TsTypeName(identifier))
        {
        }

        public TsTypeReference(TsTypeName typeName,
                               TsType[] typeArguments)
            : this(typeName)
        {
            TypeArguments = typeArguments;
        }

        public TsTypeName TypeName { get; }
        public TsType[] TypeArguments { get; } = Array.Empty<TsType>();
        public CsType CsType { get; set; }
    }
}
