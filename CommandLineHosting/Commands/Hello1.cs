using System.CommandLine;
using System.CommandLine.Invocation;
using System.CommandLine.NamingConventionBinder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CommandLineHosting.Greeters;

namespace CommandLineHosting.Commands;

public static class Hello1
{
    public static void Configure(Command parentCommand)
    {
        var hello1 = new Command("hello1", $"greet from {nameof(Hello1)}");
        hello1.AddOption(new Option<string>("--name", "name to greet"));
        hello1.Handler = CommandHandler.Create<Hello1Options, IHost>(HelloHandler);

        parentCommand.Add(hello1);
    }

    private static async Task HelloHandler(Hello1Options options, IHost host)
    {
        var greeter = host.Services.GetRequiredService<IGreeter>();
        await greeter.Greet(options.Name, nameof(Hello1));
    }

    private class Hello1Options
    {
        public string Name { get; set; } = default!;
    }
}