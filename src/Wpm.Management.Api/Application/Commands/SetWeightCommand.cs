namespace Wpm.Management.Api.Application.Commands;

public record SetWeightCommand(Guid PetId, decimal Weight);
