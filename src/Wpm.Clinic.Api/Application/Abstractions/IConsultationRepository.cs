using Wpm.Clinic.Domain.Entities;

namespace Wpm.Clinic.Api.Application.Abstractions;

public interface IConsultationRepository
{
    Task<Guid> CreateAsync(Guid patientId,CancellationToken cancellationToken = default);
    Task UpdateAsync(Consultation consultation, CancellationToken cancellationToken = default);
    Task<Consultation?> GetByIdAsync(Guid consultationId, CancellationToken cancellationToken = default);
}
