using Wpm.SharedKernel;

namespace Wpm.Clinic.Domain.Events;

public record WeightUpdate(Guid ConsultationId, decimal Weight):IDomainEvent;
