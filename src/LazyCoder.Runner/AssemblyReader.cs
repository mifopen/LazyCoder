using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace LazyCoder.Runner
{
    public static class AssemblyReader
    {
        private static bool AssemblyFilter(AssemblyName assemblyName)
        {
            return assemblyName.FullName.Contains("Elba")
                   || assemblyName.FullName == typeof(ICoder).Assembly.FullName;
        }

        public static Type[] Read(string dllPath)
        {
            var binDebugPath = Path.GetDirectoryName(dllPath);
            var rootAssembly = LoadAssembly(dllPath);

            AppDomain.CurrentDomain.AssemblyResolve += (sender,
                                                        args) => FindAssembly(args, binDebugPath);


            foreach (var assemblyName in rootAssembly.GetReferencedAssemblies()
                                                     .Where(AssemblyFilter))
            {
                try
                {
                    LoadAssembly(assemblyName);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            foreach (var assembly in loadedAssemblies.Values.ToArray())
            {
                try
                {
                    assembly.GetTypes(); // more assemblies will be loaded as side effect
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            var assemblies = AppDomain.CurrentDomain.GetAssemblies()
                                      .Where(x => AssemblyFilter(x.GetName()));
            var selectMany = assemblies.SelectMany(x =>
                                                   {
                                                       try
                                                       {
                                                           return x.GetTypes();
                                                       }
                                                       catch (Exception e)
                                                       {
                                                           return new Type[0];
                                                       }
                                                   })
                                       .ToArray();
            return selectMany;
        }

        private static Assembly LoadAssembly(AssemblyName assemblyName)
        {
            if (!loadedAssemblies.ContainsKey(assemblyName.FullName))
            {
                var assembly = Assembly.Load(assemblyName);
                loadedAssemblies.TryAdd(assembly.FullName, assembly);
            }

            return loadedAssemblies[assemblyName.FullName];
        }

        private static Assembly LoadAssembly(string assemblyPath)
        {
            var assembly = Assembly.Load(File.ReadAllBytes(assemblyPath));
            if (loadedAssemblies.ContainsKey(assembly.FullName))
                throw new Exception($"Multiple attempts to load assembly {assemblyPath}");

            loadedAssemblies.Add(assembly.FullName, assembly);
            return assembly;
        }

        private static readonly Dictionary<string, Assembly> loadedAssemblies = new Dictionary<string, Assembly>();

        private static Assembly FindAssembly(ResolveEventArgs args,
                                             string webAppBinDebug)
        {
            if (!AssemblyFilter(new AssemblyName(args.Name)))
                return null;

            if (loadedAssemblies.ContainsKey(args.Name))
                return loadedAssemblies[args.Name];


            var assemblyPath = Path.Combine(webAppBinDebug, new AssemblyName(args.Name).Name + ".dll");
            if (!File.Exists(assemblyPath))
                return null;

            return LoadAssembly(assemblyPath);
        }
    }
}
