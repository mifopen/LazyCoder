using System;
using System.IO;
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
                       Directory = csDeclaration.Namespace
                                                .Replace("Kontur.", "")
                                                .Replace(csDeclaration.CsType.OriginalType.Assembly.GetName().Name,
                                                         "")
                                                .Trim('.')
                                                .Replace('.', Path.DirectorySeparatorChar),
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
                                           .Select(x => new TsInterfaceProperty
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
                                               .Select(x => new TsInterfaceProperty
                                                            {
                                                                Name = x.Name,
                                                                Type = TsType.From(x.Type)
                                                            })
                   };
        }
    }
}
