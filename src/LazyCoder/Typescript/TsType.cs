using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using LazyCoder.CSharp;

namespace LazyCoder.Typescript
{
    public abstract class TsType
    {
        public static TsType From(CsType csType,
                                  bool forceNullable = false)
        {
            return From(csType.OriginalType, forceNullable);
        }

        private static readonly List<ICustomTypeConverter> customTypeConverters =
            new List<ICustomTypeConverter>();

        internal static void RegisterCustomTypeConverters(IEnumerable<ICustomTypeConverter> converters)
        {
            customTypeConverters.AddRange(converters);
        }

        private static TsType From(Type type,
                                   bool forceNullable = false)
        {
            if (Helpers.IsNullable(type) || forceNullable)
            {
                return new TsUnionType(From(Helpers.UnwrapNullable(type)),
                                       new TsNull());
            }

            if (Helpers.IsEnumerable(type))
            {
                return new TsArrayType { ElementType = From(Helpers.UnwrapEnumerableType(type)) };
            }

            if (typeof(Delegate).IsAssignableFrom(type))
            {
                return TsPredefinedType.Any();
            }

            if (type.IsGenericType)
            {
                if (type.GetGenericTypeDefinition() == typeof(Dictionary<,>)
                    || type.GetGenericTypeDefinition() == typeof(IDictionary<,>))
                {
                    var arguments = type.GetGenericArguments();
                    var keyType = arguments[0];
                    var valueType = arguments[1];
                    var indexSignature = keyType.IsNumber()
                                             ? TsIndexSignature.ByNumber(From(valueType))
                                             : TsIndexSignature.ByString(From(valueType));
                    return new TsObjectType { Members = new[] { indexSignature } };
                }

                var typeArguments = type.GetGenericArguments()
                                        .Select(x => From(x))
                                        .ToArray();
                return new TsTypeReference(Helpers.GetName(type),
                                           typeArguments) { CsType = new CsType(type) };
            }

            if (type == typeof(NameValueCollection))
            {
                return new TsObjectType
                       {
                           Members = new[] { TsIndexSignature.ByString(TsPredefinedType.String()) }
                       };
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
            if (type.IsNumber())
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

            return new TsTypeReference(Helpers.GetName(type)) { CsType = new CsType(type) };
        }
    }
}
