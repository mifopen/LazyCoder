using System;
using System.Collections.Generic;
using LazyCoder.CSharp;

namespace LazyCoder.Typescript
{
    public abstract class TsType
    {
        public static TsType From(CsType csType)
        {
            return From(csType.OriginalType);
        }

        private static readonly List<ICustomTypeConverter> customTypeConverters =
            new List<ICustomTypeConverter>();

        internal static void RegisterCustomTypeConverters(ICustomTypeConverter[] converters)
        {
            customTypeConverters.AddRange(converters);
        }

        private static TsType From(Type type)
        {
            if (Helpers.IsNullable(type))
            {
                return new TsUnionType(From(Helpers.UnwrapNullable(type)),
                                       new TsNull());
            }

            if (Helpers.IsEnumerable(type))
            {
                return new TsArrayType { ElementType = From(Helpers.UnwrapEnumerableType(type)) };
            }

            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Dictionary<,>))
            {
                var arguments = type.GetGenericArguments();
                var keyType = arguments[0];
                var valueType = arguments[1];
                var indexSignature = keyType == typeof(int)
                                         ? TsIndexSignature.ByNumber(From(valueType))
                                         : TsIndexSignature.ByString(From(valueType));
                return new TsObjectType { Members = new[] { indexSignature } };
            }

            foreach (var customTypeConverter in customTypeConverters)
            {
                if (customTypeConverter.TryConvert(type, out var tsType))
                {
                    return tsType;
                }
            }

            return FromInternal(type);
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
            if (type == typeof(object))
                return TsPredefinedType.Any();
            if (type == typeof(Type))
                return TsPredefinedType.String();

            return new TsTypeReference(type.Name) { CsType = new CsType(type) };
        }
    }
}
