using Wpm.Clinic.Api.Application.Abstractions;
using Wpm.Clinic.Api.Infrastructure.Persistence;
using Wpm.Clinic.Domain.Entities;

namespace Wpm.Clinic.Api.Infrastructure.Repositories;

public class ConsultationRepository(ClinicDbContext context) : IConsultationRepository
{
    public async Task<Guid> CreateAsync(Guid patientId,CancellationToken cancellationToken = default)
    {
        var newConsultation = new Consultation(patientId);
        await context.Consultations.AddAsync(newConsultation, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return newConsultation.Id;
    }

    public async Task<Consultation?> GetByIdAsync(Guid consultationId, CancellationToken cancellationToken = default)
    {
        return await context.Consultations.FindAsync([consultationId], cancellationToken);
    }

    public async Task UpdateAsync(Consultation consultation, CancellationToken cancellationToken = default)
    {
        await context.SaveChangesAsync(cancellationToken);
    }
}
