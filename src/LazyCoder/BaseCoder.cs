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

            return new TsInterface
                   {
                       CsType = csClass.CsType,
                       Name = csClass.Name,
                       ExportKind = TsExportKind.Named,
                       TypeParameters = csClass.TypeParameters,
                       Base = csClass.CsType.OriginalType.BaseType == typeof(object)
                                  ? Array.Empty<TsType>()
                                  : new[]
                                    {
                                        TsType.From(new CsType(csClass.CsType.OriginalType
                                                                      .BaseType))
                                    },
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
                       Properties = csInterface.Members
                                               .Where(x => !x.IsStatic)
                                               .OfType<CsProperty>()
                                               .Select(Rewrite)
                                               .ToArray()
                   };
        }

        protected virtual TsInterface Rewrite(CsStruct csStruct)
        {
            return new TsInterface
                   {
                       CsType = csStruct.CsType,
                       Name = csStruct.Name,
                       ExportKind = TsExportKind.Named,
                       TypeParameters = csStruct.TypeParameters,
                       Properties = csStruct.Members
                                            .Where(x => !x.IsStatic)
                                            .OfType<CsProperty>()
                                            .Select(Rewrite)
                                            .ToArray()
                   };
        }

        protected virtual TsTypeMember? Rewrite(CsMember csTypeMember)
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
