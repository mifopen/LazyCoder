using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using LazyCoder.Typescript;

namespace LazyCoder.TestDll
{
    public class TestControllerCoder : ICoder
    {
        public IEnumerable<TsFile> Rewrite(Type[] types)
        {
            var controllers = types.Where(x => x.Name.EndsWith("Controller"));
            return controllers.Select(RewriteController);
        }

        private static TsFile RewriteController(Type controllerType)
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
                                              Declarations = controllerType.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                                                                           .Where(m => !typeof(object)
                                                                                        .GetMethods()
                                                                                        .Select(me => me.Name)
                                                                                        .Contains(m.Name))
                                                                           .Select(RewriteMethod)
                                          }
                                      }
                   };
        }

        private static TsFunction RewriteMethod(MethodInfo method)
        {
            return new TsFunction
                   {
                       Name = new TsName { Value = method.Name },
                       ExportKind = TsExportKind.Named,
                       ReturnType = TsType.From(method.ReturnType),
                       Parameters = method.GetParameters()
                                          .Select(x => new TsFunctionParameter
                                                       {
                                                           Name = x.Name,
                                                           Type = TsType.From(x.ParameterType)
                                                       }),
                       Body = "// some body"
                   };
        }
    }
}
