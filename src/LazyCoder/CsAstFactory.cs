using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using LazyCoder.CSharp;

namespace LazyCoder
{
    //todo cyclic ast
    public static class CsAstFactory
    {
        public static IEnumerable<CsType> Create(IEnumerable<Type> types)
        {
            return types.Select(Create);
        }

        public static CsType Create(Type type)
        {
            if (type.IsEnum)
            {
                return new CsEnum
                       {
                           Name = type.Name,
                           Namespace = type.Namespace,
                           OriginalType = type,
                           Values = type.GetFields()
                                        .Where(f => f.Name != "value__")
                                        .Select(y => new CsEnumValue
                                                     {
                                                         Name = y.Name,
                                                         Value = (int)y.GetRawConstantValue()
                                                     })
                       };
            }

            if (type.IsClass)
            {
                return new CsClass
                       {
                           Name = type.Name,
                           Namespace = type.Namespace,
                           OriginalType = type,
                           Members = type.GetMembers()
                                         .Where(m => !typeof(object)
                                                      .GetMembers()
                                                      .Select(me => me.Name)
                                                      .Contains(m.Name))
                                         .Select(Create)
                       };
            }

            if (type.IsValueType)
            {
                return new CsSimpleType
                       {
                           OriginalType = type
                       };
            }

            throw new Exception($"Type {type.Name} is unsupported");
        }

        private static CsClassMember Create(MemberInfo memberInfo)
        {
            switch (memberInfo)
            {
                case MethodInfo methodInfo:
                    return Create(methodInfo);
                default:
                    throw new ArgumentOutOfRangeException(nameof(memberInfo), memberInfo, null);
            }
        }

        private static CsMethod Create(MethodInfo methodInfo)
        {
            return new CsMethod
                   {
                       Name = methodInfo.Name,
                       IsStatic = methodInfo.IsStatic,
                       AccessModifier = GetAccessModifier(methodInfo.IsPrivate,
                                                          methodInfo.IsFamily,
                                                          methodInfo.IsPublic,
                                                          methodInfo.IsAssembly),
                       ReturnType = Create(methodInfo.ReturnType),
                       Parameters = methodInfo.GetParameters()
                                              .Select(Create)
                   };
        }

        private static CsMethodParameter Create(ParameterInfo x)
        {
            return new CsMethodParameter
                   {
                       Name = x.Name,
                       Type = Create(x.ParameterType)
                   };
        }

        private static CsAccessModifier GetAccessModifier(bool isPrivate, bool isFamily, bool isPublic, bool isAssembly)
        {
            if (isPrivate)
                return CsAccessModifier.Private;
            if (isFamily)
                return CsAccessModifier.Protected;
            if (isAssembly)
                return CsAccessModifier.Internal;
            if (isPublic)
                return CsAccessModifier.Public;
            throw new ArgumentException("Unsupported access modifier");
        }
    }
}
