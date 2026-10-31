using Wpm.SharedKernel;

namespace Wpm.Clinic.Domain.Events;

public record ConsultationStarted(Guid ConsultationId, Guid PatientId, DateTime StartedAt):IDomainEvent;
