using Wpm.Management.Domain.ValueObjects;
using Wpm.SharedKernel;

namespace Wpm.Management.Domain.Entities;

public class Pet : Entity
{
    public string Name { get; init; } = default!;
    public int Age { get; init; }  
    public string Color { get; init; } = default!;
    public Weight? Weight { get; private set; }
    public WeightClass WeightClass { get; private set; } = WeightClass.Unknown;
    public PetSex Sex { get; init; }
    public BreedId BreedId { get; init; }


    public Pet(Guid id,
        string name,
        int age,
        PetSex sex,
        string color,
        BreedId breedId)
    {
        Id = id;
        Name = name;
        Age = age;
        Sex = sex;
        Color = color;
        BreedId = breedId;
    }

    public void SetWeight(Weight weight, WeightRange idealRange)
    {
        ArgumentNullException.ThrowIfNull(weight);
        ArgumentNullException.ThrowIfNull(idealRange);

        Weight = weight;

        WeightClass = weight.Value switch
        {
            var w when w < idealRange.From => WeightClass.Underweight,
            var w when w > idealRange.To => WeightClass.Overweight,
            _ => WeightClass.Ideal
        };
    }
}
public enum PetSex
{
    Male,
    Female
}

public enum WeightClass{
    Unknown,
    Ideal,
    Underweight,
    Overweight
}