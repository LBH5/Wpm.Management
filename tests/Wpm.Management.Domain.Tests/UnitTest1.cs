using Wpm.Management.Domain.Entities;
using Wpm.Management.Domain.ValueObjects;
using Wpm.SharedKernel;

namespace Wpm.Management.Domain.Tests;

public class UnitTest1
{
    [Fact]
    public void Pet_should_be_equal()
    {
        var id = Guid.NewGuid();
        var breedId = new BreedId(Guid.NewGuid());
        var pet1 = new Pet(id,
            "Buddy",
            3,
            PetSex.Male,
            "Brown", breedId);
        var pet2 = new Pet(id,
            "Buddy",
            3,
            PetSex.Male,
            "Brown", breedId);
        Assert.True(pet1.Equals(pet2));
    }

    [Fact]
    public void Pet_should_be_equal_operator()
    {
        var id = Guid.NewGuid();
        var breedId = new BreedId(Guid.NewGuid());
        var pet1 = new Pet(id,
            "Buddy",
            3,
            PetSex.Male,
            "Brown", breedId);
        var pet2 = new Pet(id,
            "Buddy",
            3,
            PetSex.Male,
            "Brown", breedId);
        Assert.True(pet1 == pet2);
    }

    [Fact]
    public void Pet_should_not_be_equal_operator()
    {
        var id1 = Guid.NewGuid();
        var id2 = Guid.NewGuid();
        var breedId1 = new BreedId(Guid.NewGuid());
        var breedId2 = new BreedId(Guid.NewGuid());
        var pet1 = new Pet(id1,
            "Buddy",
            3,
            PetSex.Male,
            "Brown", breedId1);
        var pet2 = new Pet(id2,
            "Max",
            5,
            PetSex.Male,
            "Black", breedId2);
        Assert.True(pet1 != pet2);
    }

    [Fact]
    public void Pet_Weight_should_be_equal()
    {
        var weight1 = new Weight(10.0m);
        var weight2 = new Weight(10.0m);
        Assert.Equal(weight1, weight2);
    }

    [Fact]
    public void Weight_should_throw_exception_for_negative_value()
    {
        Assert.Throws<ArgumentException>(() => new Weight(-5.0m));
    }

    [Fact]
    public void WeightRange_should_be_equal()
    {
        var range1 = new WeightRange(5.0m, 10.0m);
        var range2 = new WeightRange(5.0m, 10.0m);
        Assert.Equal(range1, range2);
    }

    [Fact]
    public void WeightRange_should_throw_exception_for_negative_value()
    {
        Assert.Throws<ArgumentException>(() => new WeightRange(-5.0m, 10.0m));
    }

    [Fact]
    public void WeightRange_should_throw_exception_for_from_greater_than_to()
    {
        Assert.Throws<ArgumentException>(() => new WeightRange(10.0m, 5.0m));
    }

    [Fact]
    public void BreedId_should_be_valid()
    {
        var id = Guid.NewGuid();
        var breedId = new BreedId(id);
        Assert.NotNull(breedId);
    }
    [Fact]
    public void BreedId_should_throw_exception_for_nonexistent_breed()
    {

        var nonExistentBreedId = Guid.Empty; // Assuming this ID does not exist in the breed service
        var exception = Record.Exception(() => new BreedId(nonExistentBreedId));
        Assert.True(exception is ArgumentException or KeyNotFoundException);
    }

}
