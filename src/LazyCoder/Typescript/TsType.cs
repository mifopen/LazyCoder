using System;
using System.Linq;
using LazyCoder.CSharp;
using LazyCoder.Writers;

namespace LazyCoder.Typescript
{
    public class TsStringLiteralType: TsType
    {
        public string String { get; set; }
    }

    public class TsNull: TsType
    {
    }

    public abstract class TsType
    {
        public static TsType From(CsType type)
        {
            var tsType = From(TypeHelpers.UnwrapNullable(type).OriginalType);
            if (TypeHelpers.IsNullable(type))
                return new TsUnionType(tsType, new TsNull());

            return tsType;
            // return new TsTypeReference
            //        {
            //            TypeName = FromInternal(TypeHelpers.UnwrapNullable(type)),
            //            Nullable = TypeHelpers.IsNullable(type),
            //            TypeArguments = type is CsClass csClass
            //                                ? csClass.Generics.Select(From)
            //                                : Array.Empty<TsType>()
            //        };
        }

        private static TsType From(Type type)
        {
            if (type == typeof(void))
                return TsPredefinedType.Void();
            if (type == typeof(string))
                return TsPredefinedType.String();
            if (type == typeof(int)
                || type == typeof(double)
                || type == typeof(float)
                || type == typeof(decimal)
                || type == typeof(long))
                return TsPredefinedType.Number();
            if (type == typeof(bool))
                return TsPredefinedType.Boolean();
            if (type == typeof(Guid))
                return TsPredefinedType.String();
            if (type == typeof(DateTime))
                return new TsTypeReference("Date");

            return new TsTypeReference(type.Name);
            // return TypeHelpers.GetTypeName(csType);
        }
    }
}
