using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LazyCoder.CSharp;
using LazyCoder.Typescript;
using Shouldly;
using Xunit;

namespace LazyCoder.Tests.Samples.CustomCoder
{
    public class CustomCoder
    {
        [Fact]
        public void Test()
        {
            var tsFiles = Converter.Convert(new[]
                                            {
                                                typeof(FirstClass)
                                            },
                                            new[]
                                            {
                                                new Coder()
                                            });
            tsFiles.Length.ShouldBe(2);

            var firstClassFile = tsFiles.Single(x => x.Name == "FirstClass");
            var firstResult = Converter.WriteFileToString(firstClassFile);
            firstResult.ShouldBe(File.ReadAllText("../../../Samples/Simple/FirstClass.ts")
                                     .Replace("\r\n", Environment.NewLine));

            var secondClassFile = tsFiles.Single(x => x.Name == "SecondClass");
            var secondResult = Converter.WriteFileToString(secondClassFile);
            secondResult.ShouldBe(File.ReadAllText("../../../Samples/Simple/SecondClass.ts")
                                      .Replace("\r\n", Environment.NewLine));
        }

        private class Coder: ICoder
        {
            public IEnumerable<TsFile> Rewrite(IEnumerable<CsDeclaration> csDeclarations)
            {
                return csDeclarations
                       .Cast<CsClass>()
                       .Select(x => new TsFile
                                    {
                                        Name = x.Name,
                                        Directory = x.Namespace.Replace('.',
                                                                        Path
                                                                            .DirectorySeparatorChar),
                                        Imports = new[]
                                                  {
                                                      new TsImport
                                                      {
                                                          Default = "lib",
                                                          Path = "npmpackagename"
                                                      }
                                                  },
                                        Declarations = new[]
                                                       {
                                                           new TsInterface
                                                           {
                                                               CsType = x.CsType,
                                                               Name = x.Name,
                                                               ExportKind = TsExportKind.Named,
                                                               Properties = GetProperties(x)
                                                           }
                                                       }
                                    });
            }

            private static TsTypeMember[] GetProperties(CsClass csClass)
            {
                return csClass.Members
                              .OfType<CsProperty>()
                              .Select(p => new TsPropertySignature
                                           {
                                               Name = p.Name,
                                               Type = TsType.From(p.Type),
                                               Optional = false
                                           })
                              .ToArray();
            }
        }

        private class FirstClass
        {
            public string StringProperty { get; set; }
            public SecondClass SecondClassProperty { get; set; }
        }

        private class SecondClass
        {
            public int NumberProperty { get; set; }
        }
    }
}