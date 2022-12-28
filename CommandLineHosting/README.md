# CommandLineHosting

This project demonstrates a command line project that has multiple commands that take arguments to do complex tasks.
The `System.CommandLine.Hosting` package is used to create a dependency injection container and execute the commands.

Subcommands can be arbitrarily nested as needed, e.g. `dotnet run command subcommand subsubcommand --argument foo`, by extending the subcommand.

## Usage and examples:

All examples run from the current directory:

Vanilla:
```
$ dotnet run hello1 --name world
Hello world from Hello1
$ dotnet run hello2 --name world
Hello world from Hello2
```

Demonstrate dependency injection's ability to configure from appsettings by quickly overriding a setting using environment variables:
```
$ GreeterOptions__GreetingVerb=Salutations dotnet run hello1 --name world
Salutations world from Hello1
$ GreeterService__Implementation=BadGreeter dotnet run hello1 --name world
Unhandled exception. System.Exception: Go away world from Hello1
```
