namespace Wpm.Clinic.Domain.ValueObjects;

public record DrugId
{
    public Guid Value { get; init; }

    public DrugId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException("DrugId cannot be empty.", nameof(value));
        }
        Value = value;
    }

    public static implicit operator DrugId(Guid value) => new(value);
}
