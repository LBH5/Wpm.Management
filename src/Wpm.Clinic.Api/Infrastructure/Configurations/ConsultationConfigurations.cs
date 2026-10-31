using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wpm.Clinic.Domain.Entities;
using Wpm.Clinic.Domain.ValueObjects;

namespace Wpm.Clinic.Api.Infrastructure.Configurations;

public class ConsultationConfigurations : IEntityTypeConfiguration<Consultation>
{
    public void Configure(EntityTypeBuilder<Consultation> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.PatientId)
            .HasConversion(
                v => v.Value,
                v => new PatientId(v))
            .IsRequired();

        builder.OwnsOne(c => c.Diagnosis);

        builder.OwnsOne(c => c.Treatment);
        
        builder.OwnsOne(c => c.CurrentWeight);

        builder.OwnsOne(c => c.Date);

        builder.OwnsMany(c => c.AdministeredDrugs, a =>
        {
            a.WithOwner().HasForeignKey("ConsultationId");
            a.OwnsOne(d => d.DrugId);
            a.OwnsOne(d => d.Dose);
        });

        builder.OwnsMany(c => c.VitalSignsRecords, v =>
        {
            v.WithOwner().HasForeignKey("ConsultationId");
        });
    }
}
