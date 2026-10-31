using Microsoft.EntityFrameworkCore;

namespace Wpm.Clinic.Api.Infrastructure.Persistence;

public class ClinicDbContext(DbContextOptions<ClinicDbContext> options):DbContext(options)
{
    public DbSet<ConsultationEventData> Consultations => Set<ConsultationEventData>();
}

public record ConsultationEventData(Guid Id, string AggregateId,string EventName, string EventData, string AssemblyQualifiedName, DateTime CreatedAt = default); 