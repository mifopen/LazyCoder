using System;

namespace LazyCoder.Typescript
{
    public static class TypeHelpers
    {
        public static string GetTypeName(Type type)
        {
            var genericParametersIndex = type.Name.IndexOf("`");
            return genericParametersIndex == -1
                       ? type.Name
                       : type.Name.Substring(0, genericParametersIndex);
        }

        public static bool IsNullable(Type type)
        {
            return type.IsGenericType
                   && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        public static Type UnwrapNullable(Type type)
        {
            return IsNullable(type)
                       ? type.GetGenericArguments()[0]
                       : type;
        }
    }
}