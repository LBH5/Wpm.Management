namespace Wpm.Management.Domain.ValueObjects;

public record BreedId
{

    public Guid Value { get; init; }


    public static BreedId Create(Guid value)
    {
        return new BreedId(value);
    }

    public BreedId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException("BreedId cannot be empty.");
        }
        Value = value;
    }

}
