using System;
using System.Collections.Generic;
using System.Linq;

namespace LazyCoder.Typescript
{
    public static class ImportFinder
    {
        public static TsImport[] Find(TsDeclaration tsDeclaration)
        {
            switch (tsDeclaration)
            {
                case TsClass tsClass:
                    return Find(tsClass);
                case TsFunction tsFunction:
                    return Find(tsFunction).ToArray();
                case TsInterface tsInterface:
                    return Find(tsInterface);
                case TsNamespace tsNamespace:
                    return Find(tsNamespace);
                case TsEnum _:
                    return Array.Empty<TsImport>();
                default:
                    throw new ArgumentOutOfRangeException(nameof(tsDeclaration), tsDeclaration.GetType().Name, null);
            }
        }

        private static TsImport[] Find(TsClass tsClass)
        {
            return Array.Empty<TsImport>();
        }

        private static IEnumerable<TsImport> Find(TsFunction tsFunction)
        {
            return tsFunction.Parameters
                             .SelectMany(p => Convert(p.Type))
                             .Concat(Convert(tsFunction.ReturnType));
        }

        private static TsImport[] Find(TsInterface tsInterface)
        {
            return Array.Empty<TsImport>();
        }

        private static TsImport[] Find(TsNamespace tsNamespace)
        {
            return tsNamespace.Declarations.SelectMany(Find).ToArray();
        }

        private static IEnumerable<TsImport> Convert(TsType tsType)
        {
            if (!( tsType is TsTypeReference tsTypeReference ))
                yield break;

            yield return new TsImport { Named = new[] { tsTypeReference.TypeName.Identifier } };

            foreach (var typeArgument in tsTypeReference.TypeArguments)
            {
                foreach (var tsImport in Convert(typeArgument))
                {
                    yield return tsImport;
                }
            }
        }
    }
}
