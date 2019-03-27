using System;
using System.Linq;
using LazyCoder.CSharp;
using LazyCoder.Typescript;

namespace LazyCoder
{
    public static class DefaultCoder
    {
        public static TsFile Rewrite(CsDeclaration csDeclaration)
        {
            return new TsFile
                   {
                       Name = csDeclaration.Name,
                       Directory = Helpers.GetDirectory(csDeclaration),
                       Declarations = new[] { RewriteInternal(csDeclaration) }
                   };
        }

        private static TsDeclaration RewriteInternal(CsDeclaration csDeclaration)
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

        private static TsEnum Rewrite(CsEnum csEnum)
        {
            return new TsEnum
                   {
                       CsType = csEnum.CsType,
                       Name = csEnum.Name,
                       ExportKind = TsExportKind.Named,
                       Values = csEnum.Values
                                      .Select(x => new TsEnumNumberValue
                                                   {
                                                       Name = x.Name, Value = x.Value
                                                   })
                   };
        }

        private static TsInterface Rewrite(CsClass csClass)
        {
            return new TsInterface
                   {
                       CsType = csClass.CsType,
                       Name = csClass.Name,
                       ExportKind = TsExportKind.Named,
                       TypeParameters = csClass.TypeParameters,
                       Properties = csClass.Members
                                           .Where(x => !x.IsStatic)
                                           .OfType<CsProperty>()
                                           .Select(Rewrite)
                   };
        }

        private static TsInterface Rewrite(CsInterface csInterface)
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
                   };
        }

        private static TsInterface Rewrite(CsStruct csStruct)
        {
            return new TsInterface
                   {
                       CsType = csStruct.CsType,
                       Name = csStruct.Name,
                       ExportKind = TsExportKind.Named,
                       Properties = csStruct.Members
                                            .Where(x => !x.IsStatic)
                                            .OfType<CsProperty>()
                                            .Select(Rewrite)
                   };
        }

        private static TsTypeMember Rewrite(CsTypeMember csTypeMember)
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
                default:
                    throw new ArgumentOutOfRangeException(nameof(csTypeMember),
                                                          csTypeMember.GetType().Name, null);
            }
        }
    }
}