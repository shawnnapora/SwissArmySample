namespace CommandLineHosting.Greeters;

public class BadGreeter : IGreeter
{
    private readonly string _greeting;

    public BadGreeter(BadGreeterOptions greeterOptions)
    {
        _greeting = greeterOptions.GreetingVerb;
    }

    public async Task Greet(string toGreet, string greeterName)
    {
        await Task.CompletedTask;
        throw new Exception($"{_greeting} {toGreet} from {greeterName}");
    }
}

public class BadGreeterOptions
{
    public string GreetingVerb { get; set; } = default!;
}
