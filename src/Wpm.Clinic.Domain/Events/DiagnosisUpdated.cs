using Wpm.SharedKernel;

namespace Wpm.Clinic.Domain.Events;

public record DiagnosisUpdated(Guid ConsultationId, string Diagnosis):IDomainEvent;
