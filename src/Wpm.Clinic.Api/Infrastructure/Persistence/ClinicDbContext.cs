using Microsoft.EntityFrameworkCore;
using Wpm.Clinic.Domain.Entities;

namespace Wpm.Clinic.Api.Infrastructure.Persistence;

public class ClinicDbContext(DbContextOptions<ClinicDbContext> options):DbContext(options)
{
    public DbSet<Consultation> Consultations => Set<Consultation>();   
    override protected void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ClinicDbContext).Assembly);
    }
}
