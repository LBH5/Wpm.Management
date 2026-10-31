using System;
using Wpm.SharedKernel;

namespace Wpm.Clinic.Domain.Events;

public record ConsultationEnded(Guid ConsultationId, DateTime EndedAt):IDomainEvent;
