using System;
using Wpm.Clinic.Api.Application.Abstractions;
using Wpm.Clinic.Api.Application.Commands;

namespace Wpm.Clinic.Api.Application.Handlers;

public class SetTreatmentCommandHandler(IConsultationRepository consultationRepository) : ICommandHandler<SetTreatmentCommand>
{
    public async Task HandleAsync(SetTreatmentCommand command, CancellationToken cancellationToken = default)
    {
        var consultation = await consultationRepository.GetByIdAsync(command.ConsultationId, cancellationToken)
            ?? throw new KeyNotFoundException($"Consultation with ID {command.ConsultationId} not found.");
        consultation.SetTreatment(command.Treatment);
        await consultationRepository.UpdateAsync(consultation, cancellationToken);
    }
}
