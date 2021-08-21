using System;
using System.Collections.Generic;
using System.Linq;
using LazyCoder.CSharp;
using LazyCoder.Typescript;

namespace LazyCoder
{
    public abstract class BaseCoder: ICoder
    {
        public IEnumerable<TsFile> Rewrite(IEnumerable<CsDeclaration> csDeclarations)
        {
            return csDeclarations.Select(Rewrite);
        }

        public virtual TsFile Rewrite(CsDeclaration csDeclaration)
        {
            return new TsFile
                   {
                       Name = csDeclaration.Name,
                       Directory = Helpers.GetDirectory(csDeclaration),
                       Declarations = new[]
                                      {
                                          RewriteInternal(csDeclaration)
                                      }
                   };
        }

        protected virtual TsDeclaration RewriteInternal(CsDeclaration csDeclaration)
        {
            switch (csDeclaration)
            {
                case CsEnum csEnum:
                    return Rewrite(csEnum);
                case CsClass csClass:
                    return Rewrite(csClass);
                case CsInterface csInterface:
                    return Rewrite(csInterface);
                case CsStruct csStruct:
                    return Rewrite(csStruct);
                default:
                    throw new ArgumentOutOfRangeException(nameof(csDeclaration), csDeclaration,
                                                          null);
            }
        }

        protected virtual TsEnum Rewrite(CsEnum csEnum)
        {
            return new TsEnum
                   {
                       CsType = csEnum.CsType,
                       Name = csEnum.Name,
                       ExportKind = TsExportKind.Named,
                       Values = csEnum.Values
                                      .Select(Rewrite)
                                      .ToArray()
                   };
        }

        protected virtual TsEnumValue Rewrite(CsEnumValue x)
        {
            return new TsEnumNumberValue
                   {
                       Name = x.Name, Value = x.Value
                   };
        }

        protected virtual TsInterface Rewrite(CsClass csClass)
        {
            var properties = csClass.Members
                .Where(x => !x.IsStatic)
                .OfType<CsProperty>()
                .Select(Rewrite)
                .ToArray();

            var fields = csClass.Members
                .OfType<CsField>()
                .Select(Rewrite)
                .ToArray();

            var baseTypes = new List<Type>();
            if (csClass.CsType.OriginalType.BaseType != typeof(object))
            {
                baseTypes.Add(csClass.CsType.OriginalType.BaseType);
            }

            baseTypes.AddRange(GetInterfaces(csClass.CsType.OriginalType, false));

            return new TsInterface
                   {
                       CsType = csClass.CsType,
                       Name = csClass.Name,
                       ExportKind = TsExportKind.Named,
                       TypeParameters = csClass.TypeParameters,
                       Base = baseTypes.Select(x=> TsType.From(new CsType(x))).ToArray(),
                       Properties = properties.Concat(fields).ToArray()
                   };
        }

        protected virtual TsInterface Rewrite(CsInterface csInterface)
        {
            return new TsInterface
                   {
                       CsType = csInterface.CsType,
                       Name = csInterface.Name,
                       ExportKind = TsExportKind.Named,
                       TypeParameters = csInterface.TypeParameters,
                       Base = GetInterfaces(csInterface.CsType.OriginalType, false)
                           .Select(x=> TsType.From(new CsType(x)))
                           .ToArray(),
                       Properties = csInterface.Members
                                               .Where(x => !x.IsStatic)
                                               .OfType<CsProperty>()
                                               .Select(Rewrite)
                                               .ToArray()
                   };
        }

        protected virtual TsInterface Rewrite(CsStruct csStruct)
        {
            var baseTypes = new List<Type>();
            if (csStruct.CsType.OriginalType.BaseType != typeof(object))
            {
                baseTypes.Add(csStruct.CsType.OriginalType.BaseType);
            }

            baseTypes.AddRange(GetInterfaces(csStruct.CsType.OriginalType, false));

            return new TsInterface
                   {
                       CsType = csStruct.CsType,
                       Name = csStruct.Name,
                       ExportKind = TsExportKind.Named,
                       TypeParameters = csStruct.TypeParameters,
                       Base = baseTypes.Select(x=> TsType.From(new CsType(x))).ToArray(),
                       Properties = csStruct.Members
                                            .Where(x => !x.IsStatic)
                                            .OfType<CsProperty>()
                                            .Select(Rewrite)
                                            .ToArray()
                   };
        }

        private static IEnumerable<Type> GetInterfaces(Type type, bool includeInherited)
        {
            if (includeInherited || type.BaseType == null)
            {
                return type.GetInterfaces();
            }

            return type.GetInterfaces().Except(type.BaseType.GetInterfaces());
        }

        protected virtual TsTypeMember? Rewrite(CsTypeMember csTypeMember)
        {
            switch (csTypeMember)
            {
                case CsProperty csProperty:
                    var forceNullable = csProperty.Attributes
                                                  .Any(a => a.Name.Contains("CanBeNull"));
                    return new TsPropertySignature
                           {
                               Name = csProperty.Name,
                               Type = TsType.From(csProperty.Type,
                                                  forceNullable)
                           };
                case CsField csField:
                    if (csField.Value == null)
                        return null;

                    return new TsPropertySignature
                    {
                        Name = csField.Name,
                        Type = TsType.FromLiteral(csField.Value)
                    };
                default:
                    throw new ArgumentOutOfRangeException(nameof(csTypeMember),
                                                          csTypeMember.GetType().Name, null);
            }
        }
    }
}
