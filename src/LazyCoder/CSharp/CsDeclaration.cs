using System;
using System.Collections.Generic;
using System.Linq;

namespace LazyCoder.CSharp
{
    public abstract class CsDeclaration
    {
        public CsDeclaration(Type type)
        {
            Name = Helpers.GetName(type);
            Namespace = type.Namespace;
            CsType = new CsType(type);
            Attributes = type.CustomAttributes
                             .Select(x => new CsAttribute
                                          {
                                              Name = x.AttributeType.Name,
                                              OriginalType = x.AttributeType
                                          })
                             .ToArray();
        }

        public string Name { get; set; }
        public string Namespace { get; set; }
        public IEnumerable<CsAttribute> Attributes { get; set; } = Array.Empty<CsAttribute>();
        public CsType CsType { get; set; }
    }
}
