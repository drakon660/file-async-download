namespace AsyncEnumerable.Core;

public class Flat
{
    public string Name { get; set; }
}

public record Deep(Flat Flat);



