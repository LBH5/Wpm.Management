using Wpm.Management.Domain.ValueObjects;
using Wpm.SharedKernel;

namespace Wpm.Management.Domain.Entities;

public class Breed: Entity
{
    public string Name { get; init; } = default!;
    public string Description { get; init; } = default!;

    public WeightRange MaleIdealWeight { get; init; }
    public WeightRange FemaleIdealWeight { get; init; }

    public Breed(Guid id,
        string name,
        string description,
        WeightRange maleIdealWeight,
        WeightRange femaleIdealWeight)
    {
        Id = id;
        Name = name;
        Description = description;
        MaleIdealWeight = maleIdealWeight;
        FemaleIdealWeight = femaleIdealWeight;
    }
}


