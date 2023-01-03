// See https://aka.ms/new-console-template for more information


var validCameFromJsonBindingThing = new DtoThing
{
    MightBeNull = null,
    ShouldNotBeNull = "not null"
};

Console.WriteLine(validCameFromJsonBindingThing.ToModel().ShouldNotBeNull);

var invalidCameFromJsonBindingThing = new DtoThing
{
    MightBeNull = null,
    ShouldNotBeNull = null! // your json data didn't have this, this time.
};

Console.WriteLine(invalidCameFromJsonBindingThing.ToModel().ShouldNotBeNull);

public class DtoThing
{
    public string? MightBeNull { get; set; }
    public string ShouldNotBeNull { get; set; } = default!;
}

public readonly record struct ModelThing(string? MightBeNull, string ShouldNotBeNull);

public static class ModelExtensions
{
    public static ModelThing ToModel(this DtoThing dtoThing)
    {
        return new ModelThing(
            dtoThing.MightBeNull,
            dtoThing.ShouldNotBeNull ?? throw new ArgumentNullException(nameof(dtoThing.ShouldNotBeNull)));
    }
}