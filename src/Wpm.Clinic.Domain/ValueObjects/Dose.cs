namespace Wpm.Clinic.Domain.ValueObjects;

public record Dose
{
    public decimal Quantity { get; init; }
    public UnitOfMeasure UnitOfMeasure { get; init; }

    public Dose(decimal quantity, UnitOfMeasure unitOfMeasure)
    {
        if (quantity <= 0)
        {
            throw new ArgumentException("Dose quantity must be greater than zero.", nameof(quantity));
        }
        Quantity = quantity;
        UnitOfMeasure = unitOfMeasure;
    }
}

public enum UnitOfMeasure
{
    Milligram,
    Milliliter,
    Tablet,
    Capsule
}