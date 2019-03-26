using System;
using System.Linq;
using LazyCoder.CSharp;
using LazyCoder.Typescript;

namespace LazyCoder
{
    internal static class DefaultCoder
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
                       Properties = csClass.Members
                                           .OfType<CsProperty>()
                                           .Select(x => new TsPropertySignature
                                                        {
                                                            Name = x.Name,
                                                            Type = TsType.From(x.Type)
                                                        })
                   };
        }

        private static TsInterface Rewrite(CsInterface csInterface)
        {
            return new TsInterface
                   {
                       CsType = csInterface.CsType,
                       Name = csInterface.Name,
                       ExportKind = TsExportKind.Named,
                       Properties = csInterface.Members
                                               .OfType<CsProperty>()
                                               .Select(x => new TsPropertySignature
                                                            {
                                                                Name = x.Name,
                                                                Type = TsType.From(x.Type)
                                                            })
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
                                            .OfType<CsProperty>()
                                            .Select(x => new TsPropertySignature
                                                         {
                                                             Name = x.Name,
                                                             Type = TsType.From(x.Type)
                                                         })
                   };
        }
    }
}
