using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LazyCoder.CSharp;
using LazyCoder.Typescript;
using LazyCoder.Writers;

namespace LazyCoder
{
    public static class Converter
    {
        public static TsFile[] Convert(Type[] types,
                                       ICoder[] coders)
        {
            var csDeclarations = CsDeclarationFactory.Create(types).ToArray();
            var tsFiles = coders.SelectMany(coder => coder.Rewrite(csDeclarations))
                                .ToArray();
            var resolutionContext = new ResolutionContext();
            var tsFilesToWrite = EnsureDependencies(tsFiles, resolutionContext);
            return tsFilesToWrite.Concat(resolutionContext.DependencyTsFiles).ToArray();
        }

        public static void WriteFile(string outputDirectory,
                                     TsFile tsFile)
        {
            var directory = Path.Combine(Path.GetFullPath(outputDirectory), tsFile.Directory);
            Directory.CreateDirectory(directory);
            var writerContext = new WriterContext();
            writerContext.Write(tsFile);
            var content = writerContext.GetResult();
            File.WriteAllText(Path.Combine(directory, tsFile.Name + ".ts"), content);
        }

        private static TsFile[] EnsureDependencies(TsFile[] tsFiles,
                                                   ResolutionContext
                                                       baseResolutionContext)
        {
            var resolutionContext = baseResolutionContext.Add(tsFiles.SelectMany(GetExports));
            foreach (var tsFile in tsFiles)
            {
                var dependencies = GetDependencies(tsFile);
                var imports = Resolve(dependencies, resolutionContext);
                tsFile.Imports = tsFile.Imports.Concat(imports);
            }

            return tsFiles.ToArray();
        }


        private static Export[] GetExports(TsFile tsFile)
        {
            return tsFile.Declarations
                         .SelectMany(GetExports)
                         .ToArray();

            IEnumerable<Export> GetExports(TsDeclaration tsDeclaration)
            {
                switch (tsDeclaration)
                {
                    case TsClass _:
                    case TsInterface _:
                    case TsEnum _:
                        return new[] { new Export(tsDeclaration, tsFile) };
                    case TsFunction _:
                        return Array.Empty<Export>();
                    case TsNamespace tsNamespace:
                        return tsNamespace.Declarations.SelectMany(GetExports);
                    default:
                        throw new ArgumentOutOfRangeException(nameof(tsDeclaration),
                                                              tsDeclaration.GetType().Name,
                                                              null);
                }
            }
        }

        private static Dependency[] GetDependencies(TsFile tsFile)
        {
            var exports = GetExports(tsFile);
            return tsFile.Declarations
                         .SelectMany(DependencyFinder.Find)
                         .Where(x => !exports.Select(y => y.CsType.OriginalType)
                                             .Contains(x.OriginalType))
                         .Select(x => new Dependency(x, tsFile))
                         .DistinctBy(x => x.CsType.OriginalType)
                         .ToArray();
        }

        private static TsImport[] Resolve(Dependency[] dependencies,
                                          ResolutionContext resolutionContext)
        {
            return dependencies
                   .Select(d => new { Dependency = d, Export = GetExportFor(d) })
                   .Select(x => new TsImport
                                {
                                    Named = new[] { x.Export.Name },
                                    Path = Helpers.GetPathFromAToB(x.Dependency.Path,
                                                                   x.Export.Path)
                                           + "/" + x.Export.Name
                                })
                   .ToArray();

            Export GetExportFor(Dependency dependency)
            {
                var export = resolutionContext.GetExportFor(dependency);
                if (export != null)
                {
                    return export;
                }

                var csDeclaration = CsDeclarationFactory.Create(dependency.CsType.OriginalType);
                var tsFile = DefaultCoder.Rewrite(csDeclaration);
                var tsFiles = EnsureDependencies(new[] { tsFile }, resolutionContext);
                resolutionContext.AddFiles(tsFiles);
                return resolutionContext.GetExportFor(dependency);
            }
        }

        private class ResolutionContext
        {
            private readonly List<Export> exports = new List<Export>();

            public List<TsFile> DependencyTsFiles { get; } = new List<TsFile>();

            public Export GetExportFor(Dependency dependency)
            {
                return exports.SingleOrDefault(e => e.CsType.OriginalType ==
                                                    dependency.CsType.OriginalType);
            }

            public ResolutionContext Add(IEnumerable<Export> newExports)
            {
                exports.AddRange(newExports);
                return this;
            }

            public void AddFiles(IEnumerable<TsFile> tsFiles)
            {
                DependencyTsFiles.AddRange(tsFiles);
            }
        }

        private class Export
        {
            public Export(TsDeclaration tsDeclaration,
                          TsFile tsFile)
            {
                Path = DirectoryToPath(tsFile.Directory);
                CsType = tsDeclaration.CsType;
                Name = tsDeclaration.Name;
            }

            public string Name { get; }
            public CsType CsType { get; }
            public string[] Path { get; }

            public override string ToString()
            {
                return Name;
            }
        }

        private class Dependency
        {
            public Dependency(CsType csType,
                              TsFile tsFile)
            {
                CsType = csType;
                Path = DirectoryToPath(tsFile.Directory);
            }

            public CsType CsType { get; }
            public string[] Path { get; }

            public override string ToString()
            {
                return CsType.ToString();
            }
        }

        private static string[] DirectoryToPath(string directory)
        {
            return string.IsNullOrEmpty(directory) || directory == "."
                       ? Array.Empty<string>()
                       : directory.Split(Path.DirectorySeparatorChar);
        }
    }
}
