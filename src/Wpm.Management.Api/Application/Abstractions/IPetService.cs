using Wpm.Management.Api.Application.Commands;

namespace Wpm.Management.Api.Application.Abstractions;

public interface IPetService
{
    Task CreatePetAsync(CreatePetCommand command);
    Task UpdatePetWeightAsync(SetWeightCommand command);
}
