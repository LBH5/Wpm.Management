using Wpm.Management.Api.Application.Abstractions;
using Wpm.Management.Api.Application.Commands;
using Wpm.Management.Domain.Entities;

namespace Wpm.Management.Api.Application.Handlers;

public class SetWeightCommandHandler(IPetRepository petRepository, IBreedService breedService) : ICommandHandler<SetWeightCommand>
{
    public async Task HandleAsync(SetWeightCommand command)
    {
        var pet = await petRepository.GetByIdAsync(command.PetId) ?? throw new KeyNotFoundException(
                $"Pet with ID {command.PetId} was not found.");
        var breed = await breedService.GetBreedByIdAsync(pet.BreedId.Value) ?? throw new KeyNotFoundException(
                $"Breed with ID {pet.BreedId} was not found.");

        var idealWeight = pet.Sex switch
        {
            PetSex.Male => breed.MaleIdealWeight,
            PetSex.Female => breed.FemaleIdealWeight,
            _ => throw new ArgumentOutOfRangeException($"Unexpected sex value: {pet.Sex}")
        };
        pet.SetWeight(command.Weight, idealWeight);
        await petRepository.UpdateAsync(pet);
    }
}
