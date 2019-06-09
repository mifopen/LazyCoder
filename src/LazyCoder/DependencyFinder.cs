using System;
using System.Collections.Generic;
using System.Linq;
using LazyCoder.CSharp;
using LazyCoder.Typescript;

namespace LazyCoder
{
    internal static class DependencyFinder
    {
        public static CsType[] Find(TsDeclaration tsDeclaration)
        {
            return FindInternal(tsDeclaration)
                   .Where(x => x != null)
                   .SelectMany(Unwrap)
                   .OfType<TsTypeReference>()
                   .Select(x => x.CsType)
                   .Where(x => x != null)
                   .Where(x => !x.OriginalType.IsGenericParameter)
                   .ToArray();
        }

        private static IEnumerable<TsType> FindInternal(TsDeclaration tsDeclaration)
        {
            switch (tsDeclaration)
            {
                case TsClass tsClass:
                    return tsClass.Base;
                case TsEnum _:
                    return Array.Empty<TsType>();
                case TsFunction tsFunction:
                    return tsFunction.Parameters.Select(x => x.Type)
                                     .Append(tsFunction.ReturnType);
                case TsInterface tsInterface:
                    return tsInterface.Base
                                      .Concat(tsInterface.Properties
                                                         .OfType<TsPropertySignature>()
                                                         .Select(x => x.Type));
                case TsNamespace tsNamespace:
                    return tsNamespace.Declarations.SelectMany(FindInternal);
                default:
                    throw new ArgumentOutOfRangeException(nameof(tsDeclaration), tsDeclaration,
                                                          null);
            }
        }

        private static IEnumerable<TsType> Unwrap(TsType tsType)
        {
            switch (tsType)
            {
                case TsUnionType tsUnionType:
                    return tsUnionType.Types.SelectMany(Unwrap);
                case TsArrayType tsArrayType:
                    return new[]
                           {
                               tsArrayType.ElementType
                           };
                case TsObjectType tsObjectType:
                    return tsObjectType.Members
                                       .Select(UnwrapTsTypeMember)
                                       .SelectMany(Unwrap);
                case TsTypeReference tsTypeReference:
                    return tsTypeReference.TypeArguments
                                          .SelectMany(Unwrap)
                                          .Append(tsTypeReference);
                case TsNull _:
                case TsPredefinedType _:
                case TsStringLiteralType _:
                    return new[]
                           {
                               tsType
                           };
                default:
                    throw new ArgumentOutOfRangeException(nameof(tsType),
                                                          tsType.GetType().Name,
                                                          null);
            }
        }

        private static TsType UnwrapTsTypeMember(TsTypeMember x)
        {
            switch (x)
            {
                case TsIndexSignature tsIndexSignature:
                    return tsIndexSignature.ValueType;
                case TsPropertySignature tsPropertySignature:
                    return tsPropertySignature.Type;
                default:
                    throw new ArgumentOutOfRangeException(nameof(x),
                                                          x.GetType().Name,
                                                          null);
            }
        }
    }
}
