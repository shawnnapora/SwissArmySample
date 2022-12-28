using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Invocation;
using System.CommandLine.Hosting;
using System.CommandLine.Parsing;
using CommandLineHosting.Greeters;
using CommandLineHosting.Commands;

namespace CommandLineHosting;

class Program
{
    static async Task Main(string[] args)
    {
        await BuildCommandLine()
            .UseHost(_ => Host.CreateDefaultBuilder(),
                host =>
                {
                    host.ConfigureAppConfiguration(configuration =>
                        {
                            configuration
                                .AddJsonFile("appsettings.json")
                                .AddEnvironmentVariables();
                        })
                        .ConfigureServices((hostContext, services) =>
                        {
                            BadGreeterOptions badGreeterOptions = new();
                            hostContext.Configuration.GetRequiredSection(nameof(BadGreeterOptions))
                                .Bind(badGreeterOptions);
                            services.AddSingleton(badGreeterOptions);

                            GreeterOptions greeterOptions = new();
                            hostContext.Configuration.GetRequiredSection(nameof(GreeterOptions))
                                .Bind(greeterOptions);
                            services.AddSingleton(greeterOptions);

                            switch (hostContext.Configuration.GetRequiredSection("GreeterService").GetValue<string>("Implementation"))
                            {
                                case "BadGreeter":
                                    services.AddTransient<IGreeter, BadGreeter>();
                                    break;
                                case "Greeter":
                                    services.AddTransient<IGreeter, Greeter>();
                                    break;
                                default:
                                    throw new ArgumentException("Unexpected greeter implementation");
                            }
                        });
                })
            .Build()
            .InvokeAsync(args);
    }

    private static CommandLineBuilder BuildCommandLine()
    {
        var rootCommand = new RootCommand("CommandLineHosting Sample");
        Hello1.Configure(rootCommand);
        Hello2.Configure(rootCommand);
        return new CommandLineBuilder(rootCommand);
    }
}
