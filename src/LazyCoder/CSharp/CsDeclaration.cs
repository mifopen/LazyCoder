using System;
using System.Linq;

namespace LazyCoder.CSharp
{
    public abstract class CsDeclaration
    {
        protected CsDeclaration(Type type)
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

        public string Name { get; }
        public string Namespace { get; }
        public CsAttribute[] Attributes { get; } = Array.Empty<CsAttribute>();
        public CsType CsType { get; }
    }
}
