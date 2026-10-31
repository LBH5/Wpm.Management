using Wpm.SharedKernel;

namespace Wpm.Clinic.Domain.Events;

public record TreatmentUpdate(Guid ConsultationId, string Treatment):IDomainEvent;
