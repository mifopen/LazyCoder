using System;
using System.Collections.Generic;
using System.Linq;
using LazyCoder.CSharp;
using LazyCoder.Writers;

namespace LazyCoder.Typescript
{
    public class TsType
    {
        public string Name { get; set; }
        public IEnumerable<TsType> Generics { get; set; } = Array.Empty<TsType>();
        public bool Nullable { get; set; }

        public static TsType From(CsType type)
        {
            return new TsType
                   {
                       Name = GetNameFrom(TypeHelpers.UnwrapNullable(type)),
                       Nullable = TypeHelpers.IsNullable(type),
                       Generics = type is CsClass csClass
                                      ? csClass.Generics.Select(From)
                                      : Array.Empty<TsType>()
                   };
        }

        private static string GetNameFrom(CsType csType)
        {
            var type = csType.OriginalType;
            if (type == typeof(void))
                return "void";
            if (type == typeof(string))
                return "string";
            if (type == typeof(int)
                || type == typeof(double)
                || type == typeof(float)
                || type == typeof(decimal)
                || type == typeof(long))
                return "number";
            if (type == typeof(bool))
                return "boolean";
            if (type == typeof(Guid))
                return "string";
            if (type == typeof(DateTime))
                return "Date";
            return TypeHelpers.GetTypeName(csType);
        }
    }
}
