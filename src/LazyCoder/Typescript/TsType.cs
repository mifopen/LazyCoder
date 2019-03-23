using System;
using LazyCoder.CSharp;
using LazyCoder.Writers;

namespace LazyCoder.Typescript
{
    public abstract class TsType
    {
        public static TsType From(CsType csType)
        {
            return From(csType.OriginalType);
        }

        private static TsType From(Type type)
        {
            var tsType =
                FromInternal(Helpers.UnwrapEnumerableType(Helpers
                                                                  .UnwrapNullable(type)));
            if (Helpers.IsNullable(type))
                return new TsUnionType(tsType, new TsNull());

            if (Helpers.IsEnumerable(type))
                return new TsArrayType
                       {
                           ElementType = From(Helpers.UnwrapEnumerableType(type))
                       };

            return tsType;
        }

        private static TsType FromInternal(Type type)
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

            return new TsTypeReference(type.Name) { CsType = new CsType(type) };
        }
    }
}
