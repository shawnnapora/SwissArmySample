using System.CommandLine;
using System.CommandLine.NamingConventionBinder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CommandLineHosting.Greeters;

namespace CommandLineHosting.Commands;

public static class Hello2
{
    public static void Configure(Command parentCommand)
    {
        var hello1 = new Command("hello2", $"greet from {nameof(Hello2)}");
        hello1.AddOption(new Option<string>("--name", "name to greet"));
        hello1.Handler = CommandHandler.Create<Hello2Options, IHost>(HelloHandler);

        parentCommand.Add(hello1);
    }

    private static async Task HelloHandler(Hello2Options options, IHost host)
    {
        var greeter = host.Services.GetRequiredService<IGreeter>();
        await greeter.Greet(options.Name, nameof(Hello2));
    }

    private class Hello2Options
    {
        public string Name { get; set; } = default!;
    }
}