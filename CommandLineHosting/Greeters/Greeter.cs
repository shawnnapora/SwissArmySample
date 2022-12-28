namespace CommandLineHosting.Greeters;

public class Greeter : IGreeter
{
    private readonly string _greeting;

    public Greeter(GreeterOptions greeterOptions)
    {
        _greeting = greeterOptions.GreetingVerb;
    }

    public async Task Greet(string toGreet, string greeterName)
    {
        await Task.CompletedTask;
        Console.WriteLine($"{_greeting} {toGreet} from {greeterName}");
    }
}

public class GreeterOptions
{
    public string GreetingVerb { get; set; } = default!;
}
