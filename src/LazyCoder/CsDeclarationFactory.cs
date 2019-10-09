using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using LazyCoder.CSharp;

namespace LazyCoder
{
    internal static class CsDeclarationFactory
    {
        public static CsDeclaration[] Create(IEnumerable<Type> types)
        {
            return types.Select(Create)
                        .Where(x => x != null)
                        .ToArray();
        }

        public static CsDeclaration Create(Type type)
        {
            if (type.IsEnum)
            {
                return new CsEnum(type)
                       {
                           Values = type.GetFields()
                                        .Where(f => f.Name != "value__")
                                        .Select(y => new CsEnumValue
                                                     {
                                                         Name = y.Name,
                                                         Value =
                                                             Convert
                                                                 .ToInt32(y.GetRawConstantValue())
                                                     })
                                        .ToArray()
                       };
            }

            if (type.IsClass)
            {
                return new CsClass(type)
                       {
                           TypeParameters = type.IsGenericType
                                                ? type.GetGenericTypeDefinition()
                                                      .GetGenericArguments()
                                                      .Select(x => x.Name)
                                                      .ToArray()
                                                : Array.Empty<string>(),
                           Members = type.GetDefinition()
                                         .GetMembers(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                                         .Where(m => !typeof(object)
                                                      .GetMembers()
                                                      .Select(me => me.Name)
                                                      .Contains(m.Name))
                                         .Select(Create)
                                         .Where(x => x != null)
                                         .ToArray()
                       };
            }

            if (type.IsInterface)
            {
                return new CsInterface(type)
                       {
                           TypeParameters = type.IsGenericType
                                                ? type.GetGenericTypeDefinition()
                                                      .GetGenericArguments()
                                                      .Select(x => x.Name)
                                                      .ToArray()
                                                : Array.Empty<string>(),
                           Members = type.GetDefinition()
                                         .GetMembers(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                                         .Where(m => !typeof(object)
                                                      .GetMembers()
                                                      .Select(me => me.Name)
                                                      .Contains(m.Name))
                                         .Select(Create)
                                         .Where(x => x != null)
                                         .ToArray()
                       };
            }

            var isStruct = type.IsValueType && !type.IsEnum && !type.IsPrimitive;
            if (isStruct)
            {
                return new CsStruct(type)
                       {
                           TypeParameters = type.IsGenericType
                                                ? type.GetGenericTypeDefinition()
                                                      .GetGenericArguments()
                                                      .Select(x => x.Name)
                                                      .ToArray()
                                                : Array.Empty<string>(),
                           Members = type.GetDefinition()
                                         .GetMembers()
                                         .Where(m => !typeof(object)
                                                      .GetMembers()
                                                      .Select(me => me.Name)
                                                      .Contains(m.Name))
                                         .Select(Create)
                                         .Where(x => x != null)
                                         .ToArray()
                       };
            }

            return null;
        }

        private static CsTypeMember Create(MemberInfo memberInfo)
        {
            switch (memberInfo)
            {
                case MethodInfo methodInfo:
                    return Create(methodInfo);
                case PropertyInfo propertyInfo:
                    return Create(propertyInfo);
                case TypeInfo _:
                case FieldInfo _:
                case EventInfo _:
                    return null;
                default:
                    throw new ArgumentOutOfRangeException(nameof(memberInfo),
                                                          memberInfo.GetType().Name, null);
            }
        }

        private static CsMethod Create(MethodInfo methodInfo)
        {
            if (methodInfo.IsSpecialName)
                return null;

            return new CsMethod
                   {
                       Name = methodInfo.Name,
                       IsStatic = methodInfo.IsStatic,
                       IsInherited = methodInfo.DeclaringType != methodInfo.ReflectedType,
                       AccessModifier = GetAccessModifier(methodInfo.IsPrivate,
                                                          methodInfo.IsFamily,
                                                          methodInfo.IsPublic,
                                                          methodInfo.IsAssembly),
                       Attributes = methodInfo.CustomAttributes
                                              .Select(x => new CsAttribute
                                                           {
                                                               Name = x.AttributeType.Name,
                                                               OriginalType = x.AttributeType
                                                           })
                                              .ToArray(),
                       ReturnType = new CsType(methodInfo.ReturnType),
                       Parameters = methodInfo.GetParameters()
                                              .Select(Create)
                                              .ToArray(),
                       OriginalMethod = methodInfo
                   };
        }

        private static CsProperty Create(PropertyInfo propertyInfo)
        {
            var getMethod = propertyInfo.GetGetMethod();
            if (getMethod == null)
            {
                return null;
            }

            // ignoring overridden props
            if (getMethod != getMethod.GetBaseDefinition())
            {
                return null;
            }

            return new CsProperty
                   {
                       Name = propertyInfo.Name,
                       IsStatic = getMethod.IsStatic,
                       IsInherited = propertyInfo.DeclaringType != propertyInfo.ReflectedType,
                       AccessModifier = GetAccessModifier(getMethod.IsPrivate,
                                                          getMethod.IsFamily,
                                                          getMethod.IsPublic,
                                                          getMethod.IsAssembly),
                       Attributes = propertyInfo.CustomAttributes
                                                .Select(x => new CsAttribute
                                                             {
                                                                 Name = x.AttributeType.Name,
                                                                 OriginalType = x.AttributeType
                                                             })
                                                .ToArray(),
                       Type = new CsType(propertyInfo.PropertyType)
                   };
        }

        private static CsMethodParameter Create(ParameterInfo x)
        {
            return new CsMethodParameter { Name = x.Name, Type = new CsType(x.ParameterType) };
        }

        private static CsAccessModifier GetAccessModifier(bool isPrivate,
                                                          bool isFamily,
                                                          bool isPublic,
                                                          bool isAssembly)
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
