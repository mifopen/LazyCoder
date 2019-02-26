using CommandLine;

namespace LazyCoder.Runner
{
    class Program
    {
        private static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                  .WithParsed(o => Runner.Run(o.Dll, o.Output));
        }

        private class Options
        {
            [Option("dll", Default = "../../tests/LazyCoder.TestDll/bin/Debug/netstandard2.0/LazyCoder.TestDll.dll")]
            public string Dll { get; set; }

            [Option("output", Default = "./output")]
            public string Output { get; set; }
        }
    }
}
