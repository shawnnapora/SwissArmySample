namespace CommandLineHosting.Greeters;

public interface IGreeter
{
    Task Greet(string toGreet, string greeterName);
}