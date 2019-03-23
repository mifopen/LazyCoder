using System.Collections.Generic;
using System.IO;
using System.Linq;
using LazyCoder.CSharp;
using LazyCoder.Typescript;

namespace LazyCoder.Tests
{
    public class TestControllerCoder: ICoder
    {
        public IEnumerable<TsFile> Rewrite(IEnumerable<CsDeclaration> types)
        {
            var controllers = types.OfType<CsClass>().Where(x => x.Name.EndsWith("Controller"));
            return controllers.Select(RewriteController);
        }

        private static TsFile RewriteController(CsClass controllerType)
        {
            var name = controllerType.Name.Replace("Controller", "Api");
            return new TsFile
                   {
                       Name = name,
                       Directory = controllerType.Namespace.Replace('.', Path.DirectorySeparatorChar),
                       Declarations = new[]
                                      {
                                          new TsNamespace
                                          {
                                              Name = name,
                                              ExportKind = TsExportKind.Named,
                                              Declarations = controllerType
                                                             .Members
                                                             .OfType<CsMethod>()
                                                             .Where(x => x.AccessModifier == CsAccessModifier.Public
                                                                         && !x.IsStatic)
                                                             .Select(RewriteMethod)
                                          }
                                      }
                   };
        }

        private static TsFunction RewriteMethod(CsMethod method)
        {
            return new TsFunction
                   {
                       Name = method.Name,
                       ExportKind = TsExportKind.Named,
                       ReturnType = TsType.From(method.ReturnType),
                       Parameters = method.Parameters
                                          .Select(x => new TsFunctionParameter
                                                       {
                                                           Name = x.Name,
                                                           Type = TsType.From(x.Type)
                                                       }),
                       Body = "// some body"
                   };
        }
    }
}
