using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Wpm.Clinic.Api.Application.Abstractions;
using Wpm.Clinic.Api.Infrastructure.Persistence;
using Wpm.Clinic.Domain.Entities;
using Wpm.SharedKernel;

namespace Wpm.Clinic.Api.Infrastructure.Repositories;

public class ConsultationRepository(ClinicDbContext context) : IConsultationRepository
{
    public async Task SaveAsync(Consultation consultation, CancellationToken cancellationToken = default)
    {
        var aggregateId = $"consultation-{consultation.Id}";
        var changes = consultation.DomainEvents
            .Select(e => new ConsultationEventData(Guid.NewGuid(),
                aggregateId,
                e.GetType().Name,
                JsonConvert.SerializeObject(e),
                e.GetType().AssemblyQualifiedName!,
                DateTime.UtcNow
            ));
        if(!changes.Any())
        {
            return;
        }

        foreach (var change in changes)
        {
            await context.Consultations.AddAsync(change, cancellationToken);
        }
        await context.SaveChangesAsync(cancellationToken);
        consultation.ClearDomainEvents();
    }

    public async Task<Consultation?> LoadAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var aggregateId = $"consultation-{id}";
        var result = await context.Consultations
            .Where(e => e.AggregateId == aggregateId)
            .ToListAsync(cancellationToken);
        var domainEvents = result
            .Select(e =>
            {
                var assmeblyQualifiedName = e.AssemblyQualifiedName;
                var eventType = Type.GetType(assmeblyQualifiedName);
                var eventData = JsonConvert.DeserializeObject(e.EventData, eventType!);
                return eventData as IDomainEvent;
            });
        var aggregate = new Consultation(domainEvents!);
        return aggregate;
    }
}
