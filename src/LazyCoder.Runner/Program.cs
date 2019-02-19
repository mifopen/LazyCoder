using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Invocation;
using System.Threading.Tasks;

namespace LazyCoder.Runner
{
    class Program
    {
        private static async Task<int> Main(string[] args)
        {
            var parser = new CommandLineBuilder(new Command("lazy", handler: CommandHandler.Create(typeof(Runner).GetMethod(nameof(Runner.Run)))))
                         .AddOption(new Option(new[] { "-dll" },
                                               argument: new Argument<string>("../../tests/LazyCoder.TestDll/bin/Debug/netstandard2.0/LazyCoder.TestDll.dll")
                                                         { Arity = ArgumentArity.ExactlyOne }))
                         .AddOption(new Option(new[] { "-o", "-output", "-outputDirectory" },
                                               argument: new Argument<string>("./output")
                                                         { Arity = ArgumentArity.ExactlyOne }))
                         .UseExceptionHandler()
                         .Build();

            return await parser.InvokeAsync(args).ConfigureAwait(false);
        }
    }
}
