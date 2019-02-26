using System;
using LazyCoder.CSharp;

namespace LazyCoder.Typescript
{
    public static class TypeHelpers
    {
        public static string GetTypeName(CsType type)
        {
            var genericParametersIndex = type.Name.IndexOf("`");
            return genericParametersIndex == -1
                       ? type.Name
                       : type.Name.Substring(0, genericParametersIndex);
        }

        public static bool IsNullable(CsType type)
        {
            return type.OriginalType.IsGenericType
                   && type.OriginalType.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        public static CsType UnwrapNullable(CsType type)
        {
            return IsNullable(type)
                       ? CsAstFactory.Create(type.OriginalType.GetGenericArguments()[0])
                       : type;
        }
    }
}
