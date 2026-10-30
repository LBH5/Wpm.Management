using Wpm.Management.Api.Application.Abstractions;
using Wpm.Management.Api.Application.Commands;
using Wpm.Management.Domain.Entities;
using Wpm.Management.Domain.ValueObjects;

namespace Wpm.Management.Api.Application.Handlers;

public class CreatePetCommandHandler(IPetRepository petRepository) : ICommandHandler<CreatePetCommand>
{
    public async Task HandleAsync(CreatePetCommand command)
    {
                var breedId = new BreedId(command.BreedId);
        var newPet = new Pet(command.Id,
                            command.Name,
                            command.Age,
                            command.PetSex,
                            command.Color,
                            breedId);
                            
        await petRepository.CreateAsync(newPet);
    }
}
