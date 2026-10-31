using System;
using Wpm.Clinic.Api.Application.Abstractions;
using Wpm.Clinic.Api.Application.Commands;
using Wpm.Clinic.Domain.Entities;

namespace Wpm.Clinic.Api.Application.Handlers;

public class RegisterVitalSignsCommandHandler(IConsultationRepository consultationRepository) : ICommandHandler<RegisterVitalSignsCommand>
{
    public async Task HandleAsync(RegisterVitalSignsCommand command, CancellationToken cancellationToken = default)
    {
        var consultation = await consultationRepository.GetByIdAsync(command.ConsultationId, cancellationToken) ?? throw new KeyNotFoundException($"Consultation with ID {command.ConsultationId} not found");
        var vitalSigns = command.VitalSignsRecords.Select(vs => new VitalSigns(vs.RecordedAt,vs.Temperature,vs.HeartRate,vs.RespiratoryRate));
        consultation.RegisterVitalSigns(vitalSigns);
        await consultationRepository.UpdateAsync(consultation, cancellationToken);

    }
}
