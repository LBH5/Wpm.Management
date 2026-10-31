using System;
using Wpm.Clinic.Api.Application.Abstractions;
using Wpm.Clinic.Api.Application.Commands;

namespace Wpm.Clinic.Api.Application.Handlers;

public class SetDiagnosisCommandHandler(IConsultationRepository consultationRepository) : ICommandHandler<SetDiagnosisCommand>
{
    public async Task HandleAsync(SetDiagnosisCommand command, CancellationToken cancellationToken = default)
    {
        var consultation = await consultationRepository.GetByIdAsync(command.ConsultationId, cancellationToken) ?? throw new KeyNotFoundException($"Consultation with ID {command.ConsultationId} not found");
        consultation.SetDiagnosis(command.Diagnosis);
        await consultationRepository.UpdateAsync(consultation, cancellationToken);
    }
}
