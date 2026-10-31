using Wpm.Management.Api.Application.Abstractions;
using Wpm.Management.Api.Application.Commands;
using Wpm.Management.Domain.Entities;
using Wpm.Management.Domain.Events;

namespace Wpm.Management.Api.Application.Handlers;

public class SetWeightCommandHandler : ICommandHandler<SetWeightCommand>
{
    private readonly IPetRepository _petRepository;
    private readonly IBreedService _breedService;
    public SetWeightCommandHandler(IPetRepository petRepository, IBreedService breedService)
    {
        _petRepository = petRepository;
        _breedService = breedService;
        DomainEvents.PetWeightUpdated.Subscribe((petWeightUpdatedEvent) =>
        {
            // Handle the PetWeightUpdated event here
            Console.WriteLine($"Pet weight updated: PetId={petWeightUpdatedEvent.PetId}, NewWeight={petWeightUpdatedEvent.Weight}");
        });
    }
    public async Task HandleAsync(SetWeightCommand command)
    {
        var pet = await _petRepository.GetByIdAsync(command.PetId) ?? throw new KeyNotFoundException(
                $"Pet with ID {command.PetId} was not found.");
        var breed = await _breedService.GetBreedByIdAsync(pet.BreedId.Value) ?? throw new KeyNotFoundException(
                $"Breed with ID {pet.BreedId} was not found.");

        var idealWeight = pet.Sex switch
        {
            PetSex.Male => breed.MaleIdealWeight,
            PetSex.Female => breed.FemaleIdealWeight,
            _ => throw new ArgumentOutOfRangeException($"Unexpected sex value: {pet.Sex}")
        };
        pet.SetWeight(command.Weight, idealWeight);
        await _petRepository.UpdateAsync(pet);
    }
}
