using System;
using System.Collections.Generic;
using System.Linq;
using LazyCoder.CSharp;
using LazyCoder.Typescript;

namespace LazyCoder
{
    internal static class DependencyFinder
    {
        public static IEnumerable<CsType> Find(TsDeclaration tsDeclaration)
        {
            return FindInternal(tsDeclaration)
                   .SelectMany(Unwrap)
                   .OfType<TsTypeReference>()
                   .Select(x => x.CsType)
                   .Where(x => x != null);
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
            if (tsType is TsUnionType tsUnionType)
                return tsUnionType.Types.SelectMany(Unwrap);

            if (tsType is TsArrayType tsArrayType)
                return new[] { tsArrayType.ElementType };

            if (tsType is TsTypeReference tsTypeReference)
                return tsTypeReference.TypeArguments
                                      .SelectMany(Unwrap)
                                      .Append(tsTypeReference);

            return new[] { tsType };
        }
    }
}
