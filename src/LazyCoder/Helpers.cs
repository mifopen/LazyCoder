using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LazyCoder.CSharp;

namespace LazyCoder
{
    internal static class Helpers
    {
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

        public static bool IsEnumerable(Type type)
        {
            return type.IsArray ||
                   type.IsGenericType && type.GetDefinition() == typeof(IEnumerable<>);
        }

        public static Type UnwrapEnumerableType(Type type)
        {
            if (type.IsArray)
                return type.GetElementType();
            if (type.IsGenericType && type.GetDefinition() == typeof(IEnumerable<>))
                return type.GetGenericArguments().Single();
            return type;
        }

        private static Type GetDefinition(this Type type)
        {
            return type.IsGenericType && !type.IsGenericTypeDefinition
                       ? type.GetGenericTypeDefinition()
                       : type;
        }

        public static string GetPathFromAToB(string[] a,
                                             string[] b)
        {
            int commonPrefixLength = a.Zip(b, (x,
                                               y) => ( x, y ))
                                      .TakeWhile(p => p.x == p.y)
                                      .Count();
            var levelsToGoBackward = a.Length - commonPrefixLength + 1;
            var times = levelsToGoBackward - 1;
            var backwardSigns = levelsToGoBackward == 1
                                    ? "./"
                                    : levelsToGoBackward == 0
                                        ? "./" + a.Last() + "/"
                                        : string.Join("", Enumerable.Repeat("../", times));
            return ( backwardSigns + string.Join("/", b.Skip(commonPrefixLength)) ).TrimEnd('/');
        }

        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(
            this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector)
        {
            var knownKeys = new HashSet<TKey>((IEqualityComparer<TKey>)null);
            foreach (var item in source)
            {
                if (knownKeys.Add(keySelector(item)))
                {
                    yield return item;
                }
            }
        }

        public static string GetDirectory(CsDeclaration csDeclaration)
        {
            var type = csDeclaration.CsType.OriginalType;
            var assemblyName = type.Assembly.GetName().Name;
            var parts = GetTypeFullName(type)
                        .Replace($"Kontur.{assemblyName}.", "")
                        .Replace("+", ".")
                        .Split('.');
            return Path.Combine(parts.Take(parts.Length - 1).ToArray());
        }

        private static string GetTypeFullName(Type type)
        {
            var genericParametersIndex = type.FullName.IndexOf("`");
            return genericParametersIndex == -1
                       ? type.FullName
                       : type.FullName.Substring(0, genericParametersIndex);
        }
    }
}
