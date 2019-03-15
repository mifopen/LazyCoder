using System;

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
                    return Find(tsFunction);
                case TsInterface tsInterface:
                    return Find(tsInterface);
                case TsEnum _:
                case TsNamespace _:
                    return Array.Empty<TsImport>();
                default:
                    throw new ArgumentOutOfRangeException(nameof(tsDeclaration), tsDeclaration.GetType().Name, null);
            }
        }

        private static TsImport[] Find(TsClass tsClass)
        {
            return Array.Empty<TsImport>();
        }

        private static TsImport[] Find(TsFunction tsFunction)
        {
            return Array.Empty<TsImport>();
        }

        private static TsImport[] Find(TsInterface tsInterface)
        {
            return Array.Empty<TsImport>();
        }
    }
}