using Wpm.Clinic.Domain.Entities;

namespace Wpm.Clinic.Api.Application.Abstractions;

public interface IConsultationRepository
{
    Task SaveAsync(Consultation consultation, CancellationToken cancellationToken = default);
    Task<Consultation?> LoadAsync(Guid id, CancellationToken cancellationToken = default);

}
