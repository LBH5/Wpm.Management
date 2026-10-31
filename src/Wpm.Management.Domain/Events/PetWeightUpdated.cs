using Wpm.SharedKernel;

namespace Wpm.Management.Domain.Events;

public record PetWeightUpdated(Guid PetId, decimal Weight):IDomainEvent;
